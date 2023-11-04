using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.DTOs.Books.CompletedBooks;

namespace Tahseen.Api.Controllers.BooksControllers
{
    public class CompletedBooksController : BaseController
    {
        private readonly ICompletedBookService service;

        public CompletedBooksController(ICompletedBookService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CompletedBookForCreationDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.AddAsync(dto)
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long id,[FromBody]CompletedBookForUpdateDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Succes",
                Data = await this.service.ModifyAsync(id, dto)
            });

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RemoveAsync(id)
            });

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")]long id)
            => Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveByIdAsync(id)
            });

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = this.service.RetrieveAllAsync()
            });
    }
}
