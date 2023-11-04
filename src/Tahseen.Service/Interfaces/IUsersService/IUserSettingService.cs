
using Tahseen.Service.DTOs.Users.UserSettings;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IUserSettingService
    {
        public Task<UserSettingsForResultDto> AddAsync(UserSettingsForCreationDto dto);
        public Task<UserSettingsForResultDto> ModifyAsync(long Id, UserSettingsForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<UserSettingsForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<UserSettingsForResultDto>> RetrieveAllAsync();
    }
}
