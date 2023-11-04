using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }

        ///
        ///
        [Authorize(Policy = "Admins")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = _userService.RetrieveAllAsync()
            };
            return Ok(response);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync([FromRoute(Name = "id")]long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.RetrieveByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm]UserForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.AddAsync(dto)
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]long Id)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.RemoveAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromForm]UserForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
        //Added

    }
}
