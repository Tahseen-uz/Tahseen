using Microsoft.AspNetCore.Mvc;
using Tahseen.Api.Models;
using Tahseen.Service.DTOs.Rewards.UserBadges;
using Tahseen.Service.Interfaces.IRewardsService;

namespace Tahseen.Api.Controllers.RewardControllers;

public class UserBadgeController : BaseController
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
            Data = _userBadgesService.RetrieveAllAsync()
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
            Data = await _userBadgesService.RetrieveByIdAsync(Id)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody]UserBadgesForCreationDto dto)
    {
        var response = new Response()
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userBadgesService.AddAsync(dto)
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
            Data = await _userBadgesService.RemoveAsync(Id)
        };
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")]long Id, [FromBody]UserBadgesForUpdateDto dto)
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