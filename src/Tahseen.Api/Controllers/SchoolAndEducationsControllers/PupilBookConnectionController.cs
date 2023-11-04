using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.Interfaces.ISchoolAndEducation;

namespace Tahseen.Api.Controllers.SchoolAndEducationsControllers;

public class PupilBookConnectionController : BaseController
{
    private readonly IPupilBookConnectionService _pupilBookConnectionService;

    public PupilBookConnectionController(IPupilBookConnectionService pupilBookConnectionService)
    {
        _pupilBookConnectionService = pupilBookConnectionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _pupilBookConnectionService.RetrieveAllAsync()
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
            Data = await _pupilBookConnectionService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }



    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]PupilBookConnectionForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _pupilBookConnectionService.AddAsync(dto)
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
            Data = await _pupilBookConnectionService.RemoveAsync(id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long id, [FromBody]PupilBookConnectionForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _pupilBookConnectionService.ModifyAsync(id, dto)
        };
        return Ok(response);
    }
}
