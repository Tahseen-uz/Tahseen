using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.Rewards.Badge;

public class BadgeForUpdateDto
{
    public string Name { get; set; }
    public IFormFile ImageUrl { get; set; }
}