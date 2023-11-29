using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Tahseen.Api.Models;
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

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegistrationForCreationDto dto, string verificationCode)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._registrationService.AddAsync(dto, verificationCode)
            };
            return Ok(response);
        }
    }
}
