using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Rewards.UserBadges;
using Tahseen.Service.Interfaces.IRewardsService;

namespace Tahseen.Api.Controllers.RewardControllers;

[ApiController]
[Route("api[controller]")]
public class UserBadgeController : ControllerBase
{
    private readonly IUserBadgesService _userBadgesService;

    public UserBadgeController(IUserBadgesService userBadgesService)
    {
        _userBadgesService = userBadgesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = _userBadgesService.RetrieveAll()
        };
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long Id)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userBadgesService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(UserBadgesForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userBadgesService.AddAsync(dto)
        };
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long Id)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userBadgesService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync(long Id, UserBadgesForUpdateDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userBadgesService.ModifyAsync(Id, dto)
        };
        return Ok(response);
    }
}