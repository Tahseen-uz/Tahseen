using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Api.Controllers.BooksControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService service;

        public BooksController(IBookService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(BookForCreationDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.AddAsync(dto)
            });

        [HttpPut]
        public async ValueTask<IActionResult> PutAsync(long id,BookForUpdateDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.ModifyAsync(id, dto)
            });

        [HttpDelete("id")]
        public async ValueTask<IActionResult> DeleteAsync(long id) =>
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
    }
}
