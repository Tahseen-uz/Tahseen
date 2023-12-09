using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.AudioBooks.AudioBook;
using Tahseen.Service.DTOs.Narrators;
using Tahseen.Service.Interfaces.IAudioBookServices;
using Tahseen.Service.Interfaces.INurratorServices;

namespace Tahseen.Api.Controllers.NarratorsControllers;

public class NarratorsController : BaseController
{
    private readonly INarratorService _narratorService;

    public NarratorsController(INarratorService narratorService)
    {
        _narratorService = narratorService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _narratorService.RetrieveAllAsync()
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
            Data = await _narratorService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] NarratorForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _narratorService.AddAsync(dto)
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
            Data = await _narratorService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] NarratorForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _narratorService.ModifyAsync(Id, dto)
        };
        return Ok(response);
    }
}
