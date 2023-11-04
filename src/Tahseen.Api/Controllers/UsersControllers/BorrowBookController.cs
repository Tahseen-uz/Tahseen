using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Users.BorrowedBook;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Api.Controllers.UsersControllers
{
    public class BorrowBookController : BaseController
    {
        private readonly IBorrowedBookService _borrowedBookService;

        public BorrowBookController(IBorrowedBookService borrowedBookService)
        {
            _borrowedBookService = borrowedBookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = _borrowedBookService.RetrieveAllAsync()
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
                Data = await _borrowedBookService.RetrieveByIdAsync(Id)
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]BorrowedBookForCreationDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _borrowedBookService.AddAsync(dto)
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
                Data = await _borrowedBookService.RemoveAsync(Id)
            };
            return Ok(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromBody]BorrowedBookForUpdateDto dto)
        {
            var response = new Response()
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _borrowedBookService.ModifyAsync(Id, dto)
            };
            return Ok(response);
        }
    }
}
