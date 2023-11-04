using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.Interfaces.ISchoolAndEducation;
using Tahseen.Service.Services.SchoolAndEducations;

namespace Tahseen.Api.Controllers.SchoolAndEducationsControllers;

public class PupilsControllers : BaseController
{
    private readonly IPupilService _pupilService;

    public PupilsControllers(IPupilService pupilService)
    {
        this._pupilService = pupilService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _pupilService.RetrieveAllAsync()
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
            Data = await _pupilService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]PupilForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _pupilService.AddAsync(dto)
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
            Data = await _pupilService.RemoveAsync(id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long id, [FromBody]PupilForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _pupilService.ModifyAsync(id, dto)
        };
        return Ok(response);
    }
}