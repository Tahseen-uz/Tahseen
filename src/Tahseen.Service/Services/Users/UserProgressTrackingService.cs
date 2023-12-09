using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Users.UserProgressTracking;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class UserProgressTrackingService : IUserProgressTrackingService
    {
        private readonly IRepository<UserProgressTracking> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Book> _bookRepository;

        public UserProgressTrackingService(
            IMapper mapper,
            IRepository<UserProgressTracking> repository,
            IRepository<User> userRepository,
            IRepository<Book> bookRepository
            )
        {
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }
        public async Task<UserProgressTrackingForResultDto> AddAsync(UserProgressTrackingForCreationDto dto)
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

            var result = this._mapper.Map<UserProgressTracking>(dto);
            var CreatedData = await _repository.CreateAsync(result);
            return this._mapper.Map<UserProgressTrackingForResultDto>(CreatedData);
        }

        public async Task<UserProgressTrackingForResultDto> ModifyAsync(long Id, UserProgressTrackingForUpdateDto dto)
        {
            var Data = await _repository.SelectAll()
                .Where(e => e.Id == Id && e.IsDeleted == false)
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
            if (Data != null )
            {
                var MappedData = this._mapper.Map(dto, Data);
                MappedData.UpdatedAt = DateTime.UtcNow;
                var result = await this._repository.UpdateAsync(MappedData);
                return this._mapper.Map<UserProgressTrackingForResultDto>(result);
            }
            throw new TahseenException(404, "NotFound");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserProgressTrackingForResultDto>> RetrieveAllAsync()
        {
            var allData = await this._repository
                .SelectAll()
                .AsNoTracking()
                .ToListAsync();
            return this._mapper.Map<IEnumerable<UserProgressTrackingForResultDto>>(allData);
        }

        public async Task<UserProgressTrackingForResultDto> RetrieveByIdAsync(long Id)
        {
            var result = await this._repository.SelectByIdAsync(Id);
            return this._mapper.Map<UserProgressTrackingForResultDto>(result);
        }
    }
}
