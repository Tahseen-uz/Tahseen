using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class UserSettingsController : BaseController
    {
        private IUserSettingService _userSettingService { get; set; }

        public UserSettingsController(IUserSettingService userSettingService)
        {
            _userSettingService = userSettingService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]UserSettingsForCreationDto dto)
            => Ok(new Response()
            {
                Message = "Success",
                StatusCode = 200,
                Data = await _userSettingService.AddAsync(dto),
            });

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            => Ok(new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._userSettingService.RetrieveAllAsync()
            });

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")]long id)
            => Ok(new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._userSettingService.RetrieveByIdAsync(id)
            });


    }
}
