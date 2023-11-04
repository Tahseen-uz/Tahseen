using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Feedbacks.SurveyResponses;
using Tahseen.Service.Interfaces.IFeedbackService;

namespace Tahseen.Api.Controllers.FeedbackControllers;

public class SurveyResponseController : BaseController
{
    private readonly ISurveyResponseService _surveyResponseService;

    public SurveyResponseController(ISurveyResponseService surveyResponseService)
    {
        _surveyResponseService = surveyResponseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _surveyResponseService.RetrieveAllAsync()
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
            Data = await _surveyResponseService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]SurveyResponseForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _surveyResponseService.AddAsync(dto)
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
            Data = await _surveyResponseService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromBody]SurveyResponseForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _surveyResponseService.ModifyAsync(Id, dto)
        };
        return Ok(response);
    }
}