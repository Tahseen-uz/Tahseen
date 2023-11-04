using Tahseen.Service.DTOs.Users.Registration;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IRegistrationService
    {
        public Task<RegistrationForResultDto> AddAsync(RegistrationForCreationDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<RegistrationForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<RegistrationForResultDto>> RetrieveAllAsync();
    }
}
