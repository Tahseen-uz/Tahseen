using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.Interfaces.IFeedbackService;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class UserRatingsController : BaseController
    {
        private readonly IUserRatingService _userRatingService;

        public UserRatingsController(IUserRatingService userRatingService)
        {
            this._userRatingService = userRatingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userRatingService.RetrieveAllAsync()
            };
            return Ok(response);
        }


        [HttpGet("GetByUserId")]

        public async Task<IActionResult> GetByUserIdAsync()
        {
            var UserId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userRatingService.RetrieveByUserIdAsync(UserId)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] UserRatingForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _userRatingService.AddAsync(dto)
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
                Data = await _userRatingService.RemoveAsync(Id)
            };
            return Ok(response);
        }


    }
}
