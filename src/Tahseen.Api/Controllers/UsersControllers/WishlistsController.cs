using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Users.Wishlists;
using Tahseen.Service.Interfaces.IUsersService;

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
        public IQueryable<IActionResult> GetAll()
            => (IQueryable<IActionResult>)Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = this._wishlistService.RetrieveAllAsync()
            });


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._wishlistService.RemoveAsync(id)
            });
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]WishlistForCreationDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._wishlistService.AddAsync(dto)
            });


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._wishlistService.RemoveAsync(id)
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute]long id, [FromBody]WishlistForUpdateDto dto)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this._wishlistService.ModifyAsync(id,dto)
            });
    }
}
