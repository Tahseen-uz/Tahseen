using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Users;
using Tahseen.Service.DTOs.Users.UserCart;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class UserCartService : IUserCartService
    {
        private readonly IRepository<UserCart> _repository;
        private readonly IMapper _mapper;
        public UserCartService(IRepository<UserCart> Repository, IMapper Mapper)
        {
            this._repository = Repository;
            this._mapper = Mapper;
        }
        public async Task<UserCartForResultDto> AddAsync(UserCartForCreationDto dto)
        {
            var result = await this._repository.SelectAll()
                .Where(e => e.UserId == dto.UserId && e.IsDeleted == false)
                .FirstOrDefaultAsync();
            if (result != null)
            {
                throw new TahseenException(400, "User is exist");
            }
            var data = this._mapper.Map<UserCart>(dto);
            var CreatedData = await this._repository.CreateAsync(data);
            return _mapper.Map<UserCartForResultDto>(CreatedData);
        }


        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserCartForResultDto>> RetrieveAllAsync()
        {
            var AllData = this._repository.SelectAll().Where(t => t.IsDeleted == false);
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
