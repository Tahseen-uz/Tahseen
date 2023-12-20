using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Users.Wishlists;
using Tahseen.Service.Interfaces.IUsersService;
using System.Security.Claims;
using Tahseen.Domain.Entities;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class WishlistsController : BaseController
    {
      
        private readonly IWishlistService _wishlistService;
        public WishlistsController(IWishlistService wishlistService)
        {
            this._wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var UserId = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));
            var result = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _wishlistService.RetrieveAllAsync(UserId)
            };
            return Ok(result);

        }
           


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute(Name = "id")]long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Successful",
                Data = await this._wishlistService.RemoveAsync(id)
            });


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WishlistForCreationDto dto)
        {
            var UserId = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));
            var result = new Response()
            {
                StatusCode = 200,
                Message = "Successful",
                Data = await this._wishlistService.AddAsync(UserId, dto)
            };
            return Ok(result);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Successful",
                Data = await this._wishlistService.RemoveAsync(id)
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long id, [FromBody]WishlistForUpdateDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Successful",
                Data = await this._wishlistService.ModifyAsync(id,dto)
            });
    }
}
