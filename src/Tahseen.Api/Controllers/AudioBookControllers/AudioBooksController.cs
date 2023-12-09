using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.AudioBooks.AudioBook;
using Tahseen.Service.Interfaces.IAudioBookServices;

namespace Tahseen.Api.Controllers.AudioBookControllers;

public class AudioBooksController : BaseController
{
    private readonly IAudioBookService _audioBookService;

    public AudioBooksController(IAudioBookService audioBookService)
    {
        _audioBookService = audioBookService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _audioBookService.RetrieveAllAsync(@params)
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
            Data = await _audioBookService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] AudioBookForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _audioBookService.AddAsync(dto)
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
            Data = await _audioBookService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] AudioBookForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _audioBookService.ModifyAsync(Id, dto)
        };
        return Ok(response);
    }
}
