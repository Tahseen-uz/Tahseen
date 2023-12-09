using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.EBooks.EBook;
using Tahseen.Service.Interfaces.IEBookServices;

namespace Tahseen.Api.Controllers.EBookControllers;

public class EBooksController : BaseController
{
    private readonly IEBookService _eBookService;

    public EBooksController(IEBookService eBookService)
    {
        _eBookService = eBookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _eBookService.RetrieveAllAsync(@params)
        };
        return Ok(response);
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long Id)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _eBookService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] EBookForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _eBookService.AddAsync(dto)
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
            Data = await _eBookService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] EBookForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _eBookService.ModifyAsync(Id, dto)
        };
        return Ok(response);
    }
}
