using Tahseen.Service.DTOs.Users.Registration;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IRegistrationService
    {
        public Task<RegistrationForResultDto> AddAsync(RegistrationForCreationDto dto, string enterCode);
    }
}
