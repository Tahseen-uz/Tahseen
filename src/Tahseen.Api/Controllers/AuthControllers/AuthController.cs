using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Login;
using Tahseen.Service.Interfaces.IAuthService;

namespace Tahseen.Api.Controllers.AuthControllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> PostAsync(LoginDto dto)
        {
            var token = await this._authService.AuthenticateAsync(dto);
            return Ok(token);
        }
    }
}
