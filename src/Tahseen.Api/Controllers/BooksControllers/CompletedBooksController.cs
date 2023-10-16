using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.DTOs.Books.CompletedBooks;

namespace Tahseen.Api.Controllers.BooksControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedBooksController : ControllerBase
    {
        private readonly ICompletedBookService service;

        public CompletedBooksController(ICompletedBookService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async ValueTask<IActionResult> PostAsync(CompletedBookForCreationDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.AddAsync(dto)
            });

        [HttpPut]
        public async ValueTask<IActionResult> PutAsync(long id,CompletedBookForUpdateDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await this.service.ModifyAsync(id, dto)
            });

        [HttpDelete("id")]
        public async ValueTask<IActionResult> DeleteAsync(long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RemoveAsync(id)
            });

        [HttpGet("id")]
        public async ValueTask<IActionResult> GetByIdAsync(long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveByIdAsync(id)
            });

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync() =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = this.service.RetrieveAll()
            });
    }
}
