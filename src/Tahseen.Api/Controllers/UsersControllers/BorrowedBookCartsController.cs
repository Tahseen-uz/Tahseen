using Tahseen.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Tahseen.Service.Interfaces.IUsersService;
using Tahseen.Service.DTOs.Users.BorrowedBookCart;

namespace Tahseen.Api.Controllers.UsersControllers;

public class BorrowedBookCartsController : BaseController
{
    private readonly IBorrowBookCartService _borrowedBookCartService;

    public BorrowedBookCartsController(IBorrowBookCartService borrowedBookCartService)
    {
        _borrowedBookCartService = borrowedBookCartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _borrowedBookCartService.RetrieveAllAsync()
        };
        return Ok(response);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long Id)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _borrowedBookCartService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BorrowedBookCartForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _borrowedBookCartService.AddAsync(dto)
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
            Data = await _borrowedBookCartService.RemoveAsync(Id)
        };
        return Ok(response);
    }
}
