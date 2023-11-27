using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Users.UserProgressTracking;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class UserProgressTrackingsController : BaseController
    {
        private readonly IUserProgressTrackingService _userProgressTrackingService;

        public UserProgressTrackingsController(IUserProgressTrackingService userProgressTrackingService)
        {
            this._userProgressTrackingService = userProgressTrackingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._userProgressTrackingService.RetrieveAllAsync()
            };
            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userProgressTrackingService.RetrieveByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] UserProgressTrackingForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userProgressTrackingService.AddAsync(dto)
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userProgressTrackingService.RemoveAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] UserProgressTrackingForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userProgressTrackingService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
    }
}
