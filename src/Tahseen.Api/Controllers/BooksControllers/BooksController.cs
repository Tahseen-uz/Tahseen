using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.Configurations;
using System.Security.Claims;

namespace Tahseen.Api.Controllers.BooksControllers
{
    public class BooksController : BaseController
    {
        private readonly IBookService service;

        public BooksController(IBookService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm]BookForCreationDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.AddAsync(dto)
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long id,[FromForm]BookForUpdateDto dto) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.ModifyAsync(id, dto)
            });

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]long id) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RemoveAsync(id)
            });
        [HttpGet("ParticularLibraryBooks")]
        public async Task<IActionResult> GetParticularLibraryBooksAsync([FromQuery]PaginationParams @params)
        {
            var LibraryBranchId = Convert.ToInt32(HttpContext.User.FindFirstValue("LibraryBranchId"));
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveAllAsync(LibraryBranchId, @params)

            };
            return Ok(response);
        }

        [HttpGet("AllPublicLibraryBooks")]
        public async Task<IActionResult> GetAllPublicLibraryBooksAsync([FromQuery] PaginationParams @params) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveAllPublicLibraryBooksAsync(@params)
            });




        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")]long id) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveByIdAsync(id)
            });
    }
}
