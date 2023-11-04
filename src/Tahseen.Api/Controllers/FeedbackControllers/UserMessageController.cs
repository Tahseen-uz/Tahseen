using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Feedbacks.UserMessages;
using Tahseen.Service.Interfaces.IFeedbackService;

namespace Tahseen.Api.Controllers.FeedbackControllers;

public class UserMessageController : BaseController
{
    private readonly IUserMessageService _userMessageService;

    public UserMessageController(IUserMessageService userMessageService)
    {
        _userMessageService = userMessageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _userMessageService.RetrieveAllAsync()
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
            Data = await _userMessageService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]UserMessageForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userMessageService.AddAsync(dto)
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
            Data = await _userMessageService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromBody]UserMessageForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userMessageService.ModifyAsync(Id, dto)
        };
        return Ok(response);
    }
}