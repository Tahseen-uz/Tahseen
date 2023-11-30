using Tahseen.Service.DTOs.Registrations;
using Tahseen.Service.DTOs.Users.Registration;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IRegistrationService
    {
        public Task<RegistrationForResultDto> AddAsync(RegistrationForCreationDto dto);
        public Task<string> SendVerificationCodeAsync(SendVerificationCodeDto dto);
        public Task<bool> VerifyCodeAsync(VerifyCodeDto dto);

    }
}
