using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.Rewards.Badge;

public class BadgeForCreationDto
{
    public string Name { get; set; }
    public IFormFile ImageUrl { get; set; }
}