using AutoMapper;
using Tahseen.Domain.Entities;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.DTOs.Users.Fine;
using Tahseen.Service.Interfaces.IUsersService;
using System.Runtime.InteropServices;

namespace Tahseen.Service.Services.Users
{
    public class FineService : IFineService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Fine> _fineRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Book> _bookRepository;
        public FineService(
            IMapper mapper,
            IRepository<Fine> fineRepository, 
            IRepository<User> userRepository,
            IRepository<Book> bookRepository)
        {
            this._mapper = mapper;
            this._fineRepository = fineRepository;
            this._userRepository = userRepository;
            this._bookRepository = bookRepository;
        }
        public async Task<FineForResultDto> AddAsync(FineForCreationDto dto)
        {
            var user = await _userRepository.SelectAll()
                .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new TahseenException(404, "User is not found");


            var book = await _bookRepository.SelectAll()
                .Where(u => u.IsDeleted == false && u.Id == dto.BookId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (book is null)
                throw new TahseenException(404, "Book is not found");

            var MappedData = this._mapper.Map<Fine>(dto);
            var result = await this._fineRepository.CreateAsync(MappedData);
            return this._mapper.Map<FineForResultDto>(result);
        }

        public async Task<FineForResultDto> ModifyAsync(long Id, FineForUpdateDto dto)
        {
            var data = await this._fineRepository.SelectAll()
                .Where(e => e.Id == Id && !e.IsDeleted)
                .FirstOrDefaultAsync();

            var user = await _userRepository.SelectAll()
                .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new TahseenException(404, "User is not found");


            var book = await _bookRepository.SelectAll()
                .Where(u => u.IsDeleted == false && u.Id == dto.BookId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (book is null)
                throw new TahseenException(404, "Book is not found");
            if (data != null && data.IsDeleted == false)
            {
                var MappedData = this._mapper.Map(dto, data);
                MappedData.UpdatedAt = DateTime.UtcNow;
                var result = await this._fineRepository.UpdateAsync(MappedData);
                return this._mapper.Map<FineForResultDto>(result);
            }
            throw new TahseenException(404, "Fine is not found");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            var data = await this._fineRepository
                .SelectAll()
                .Where(f => f.Id == Id && !f.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data is null)
                throw new TahseenException(404, "Fine is not found");
            return await this._fineRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<FineForResultDto>> RetrieveAllAsync()
        {
            var AllData = await this._fineRepository
                .SelectAll()
                .AsNoTracking()
                .ToListAsync();
            return this._mapper.Map<IEnumerable<FineForResultDto>>(AllData);
        }

        public async Task<FineForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await this._fineRepository.
                SelectAll()
                .Where(f => f.Id == Id && !f.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data is null)
                throw new TahseenException(404, "Fine is not fount");
            return this._mapper.Map<FineForResultDto>(data);
        }
    }
}
