using AutoMapper;
using Tahseen.Domain.Entities;
using Tahseen.Service.Exceptions;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Interfaces.IUsersService;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;

namespace Tahseen.Service.Services.Users
{
    public class BorrowBookCartService : IBorrowBookCartService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<BorrowedBookCart> _repository;
        public BorrowBookCartService(
            IMapper mapper, 
            IRepository<User> userRepository,
            IRepository<BorrowedBookCart> repository)
        {
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
        }

        

        public async Task<BorrowedBookCartForResultDto> AddAsync(BorrowedBookCartForCreationDto dto)
        {
            var result = await _repository.SelectAll()
                .Where(e => e.UserId == dto.UserId && e.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (result != null)
                throw new TahseenException(400, "BorrowedBookCart is exist");

            var user = await _userRepository.SelectAll()
                .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new TahseenException(404, "User is not found");

            var data = this._mapper.Map<BorrowedBookCart>(dto);
            var result2 = await this._repository.CreateAsync(data);
            return this._mapper.Map<BorrowedBookCartForResultDto>(result2);
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<BorrowedBookCartForResultDto>> RetrieveAllAsync()
        {
            var AllData = await this._repository.SelectAll()
                .Where(t => t.IsDeleted == false)
                .Include(b => b.BorrowedBook.Where(n => n.IsDeleted == false))
                .ThenInclude(b => b.Book.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();
            return this._mapper.Map<IEnumerable<BorrowedBookCartForResultDto>>(AllData);
        }

        public async Task<BorrowedBookCartForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await this._repository.SelectAll()
                            .Where(t => t.IsDeleted == false)
                            .Include(b => b.BorrowedBook.Where(n => n.IsDeleted == false))
                            .ThenInclude(b => b.Book.IsDeleted == false)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(); 
            if (data != null && data.IsDeleted == false)
            {
                return this._mapper.Map<BorrowedBookCartForResultDto>(data);
            }
            throw new TahseenException(404, "Not Found");
        }
    }
}
