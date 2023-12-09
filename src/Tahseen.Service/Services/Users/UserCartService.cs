using AutoMapper;
using Tahseen.Domain.Entities;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Users;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class UserCartService : IUserCartService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<UserCart> _repository;
        private readonly IRepository<User> _userRepository;
        public UserCartService(
            IMapper Mapper, 
            IRepository<UserCart> Repository, 
            IRepository<User> userRepository)
        {
            this._mapper = Mapper;
            this._repository = Repository;
            this._userRepository = userRepository;
        }
        public async Task<UserCartForResultDto> AddAsync(UserCartForCreationDto dto)
        {
            var result = await this._repository.SelectAll()
                .Where(e => e.UserId == dto.UserId && e.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (result != null)
            {
                throw new TahseenException(400, "User is exist");
            }

            var user = await _userRepository.SelectAll()
                .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
                throw new TahseenException(404, "User is not found");

            var data = this._mapper.Map<UserCart>(dto);
            var CreatedData = await this._repository.CreateAsync(data);
            return _mapper.Map<UserCartForResultDto>(CreatedData);
        }


        public async Task<bool> RemoveAsync(long Id)
        {
            var data = await this._repository
                .SelectAll()
                .Where(u => u.Id == Id && !u.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data is null)
                throw new TahseenException(404, "UserCart is not found");

            return await this._repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserCartForResultDto>> RetrieveAllAsync()
        {
            var AllData = await this._repository.SelectAll()
                .Where(t => t.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();
            return this._mapper.Map<IEnumerable<UserCartForResultDto>>(AllData);
            
        }

        public async Task<UserCartForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await _repository.SelectByIdAsync(Id);
            if(data != null && data.IsDeleted == false)
            {
               return this._mapper.Map<UserCartForResultDto>(data);
            }
            throw new TahseenException(404, "Not Found");
            
        }
    }
}
