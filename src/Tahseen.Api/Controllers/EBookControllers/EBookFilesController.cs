using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.AudioBooks.AudioFile;
using Tahseen.Service.DTOs.EBooks.EBookFile;
using Tahseen.Service.Interfaces.IEBookServices;

namespace Tahseen.Api.Controllers.EBookControllers;

public class EBookFilesController : BaseController
{
    private readonly IEBookFileService _fileService;

    public EBookFilesController(IEBookFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _fileService.RetrieveAllAsync()
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
            Data = await _fileService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromForm] EBookFileForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _fileService.AddAsync(dto)
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
            Data = await _fileService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromForm] EBookFileForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _fileService.ModifyAsync(Id, dto)
        };
        return Ok(response);
    }
}
