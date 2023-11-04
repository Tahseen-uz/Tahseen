using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.Interfaces.ISchoolAndEducation;
using Tahseen.Service.Services.SchoolAndEducations;

namespace Tahseen.Api.Controllers.SchoolAndEducationsControllers;

public class SchoolBookController : BaseController
{
    private readonly ISchoolBookService _schoolBookService;

    public SchoolBookController(ISchoolBookService schoolBookService)
    {
        _schoolBookService = schoolBookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _schoolBookService.RetrieveAllAsync()
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
            Data = await _schoolBookService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]SchoolBookForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _schoolBookService.AddAsync(dto)
        };
        return Ok(response);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]long id)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _schoolBookService.RemoveAsync(id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long id, [FromBody]SchoolBookForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _schoolBookService.ModifyAsync(id, dto)
        };
        return Ok(response);
    }
}
