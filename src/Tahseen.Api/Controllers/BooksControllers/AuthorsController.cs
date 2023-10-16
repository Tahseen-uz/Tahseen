using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Api.Controllers.BooksControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService service;

        public AuthorsController(IAuthorService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(AuthorForCreationDto dto)=>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.AddAsync(dto)
            });
        [HttpPut]
        public async Task<IActionResult> PutAsync(long id,AuthorForUpdateDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.ModifyAsync(id,dto)
            });
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(long id) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RemoveAsync(id)
            });
        [HttpGet("id")]
        public async ValueTask<IActionResult> GetByIdAsync(long id) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveByIdAsync(id)
            });
        [HttpGet]
        public async ValueTask<IActionResult> GetAll() =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = this.service.RetrieveAll()
            });
    }
}
