using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Users.Fine;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class FineService : IFineService
    {
        private readonly IRepository<Fine> _fineRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Book> _bookRepository;
        public FineService(
            IRepository<Fine> fineRepository, 
            IMapper mapper,
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
            var data = await this._fineRepository.SelectAll().FirstOrDefaultAsync(e => e.Id == Id);

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
            return await this._fineRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<FineForResultDto>> RetrieveAllAsync()
        {
            var AllData = this._fineRepository.SelectAll();
            return this._mapper.Map<IEnumerable<FineForResultDto>>(AllData);
        }

        public async Task<FineForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await this._fineRepository.SelectByIdAsync(Id);
            return this._mapper.Map<FineForResultDto>(data);
        }
    }
}
