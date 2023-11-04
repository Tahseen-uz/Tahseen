using Tahseen.Service.DTOs.Login;

namespace Tahseen.Service.Interfaces.IAuthService
{
    public interface IAuthService
    {
        public Task<LoginForResultDto> AuthenticateAsync(LoginDto dto);
    }
}
