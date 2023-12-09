using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Registrations;
using Tahseen.Service.DTOs.Users.Registration;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class RegistrationController : BaseController
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostAsync([FromBody] RegistrationForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._registrationService.AddAsync(dto)
            };
            return Ok(response);
        }

        [HttpPost("SendVerificationCode")]
        public async Task<IActionResult> SendVerificationCode([FromBody] SendVerificationCodeDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._registrationService.SendVerificationCodeAsync(dto)
            };
            return Ok(response);
        }

        [HttpPost("VerifyCode")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._registrationService.VerifyCodeAsync(dto)
            };
            return Ok(response);
        }
    }
}
