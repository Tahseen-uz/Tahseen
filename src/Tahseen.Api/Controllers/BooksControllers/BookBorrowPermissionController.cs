using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Xml.Linq;
using Tahseen.Api.Models;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.DTOs.Books.BookBorrowPermission;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Api.Controllers.BooksControllers
{
    public class BookBorrowPermissionController : BaseController
    {
        private readonly IBookBorrowPermissionService service;

        public BookBorrowPermissionController(IBookBorrowPermissionService bookBorrowPermissionService)
        {
            service = bookBorrowPermissionService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BookBorrowPermissionForCreationDto dto)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));
            var CreationDto = new BookBorrowPermissionForCreationDto()
            {
                BookId = dto.BookId,
                LibraryBranchId = dto.LibraryBranchId,
                UserId = userId
            };
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.AddAsync(CreationDto)
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RemoveAsync(id)
            });
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id) =>
            Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveByIdAsync(id)
            });
        [HttpGet]
        public async Task<IActionResult> RetrieveAllBookByLibraryBranchIdAsync()
        {
            var libraryBranchId = Convert.ToInt32(HttpContext.User.FindFirstValue("LibraryBranchId"));
            var response = new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await this.service.RetrieveAllBookByLibraryBranchIdAsync(libraryBranchId)
            };
            return Ok(response);
        }

    }
}
