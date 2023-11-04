using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IUserService
    {
        public Task<UserForResultDto> AddAsync(UserForCreationDto dto);
        public Task<UserForResultDto> ModifyAsync(long Id, UserForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<UserForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<UserForResultDto>> RetrieveAllAsync();
    }
}
