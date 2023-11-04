using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Librarians;
using Tahseen.Service.DTOs.Notifications;
using Tahseen.Service.Interfaces.ILibrariansService;
using Tahseen.Service.Interfaces.INotificationServices;
using Tahseen.Service.Services.Notifications;

namespace Tahseen.Api.Controllers.NotificationsControllers;

public class NotificationController : BaseController
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] NotificationForCreationDto data)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _notificationService.AddAsync(data)
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
            Data = await _notificationService.RetrieveByIdAsync(Id)
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
            Data = await _notificationService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long Id, [FromBody] NotificationForUpdateDto data)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _notificationService.ModifyAsync(Id, data)
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _notificationService.RetrieveAllAsync()
        };
        return Ok(response);
    }


}