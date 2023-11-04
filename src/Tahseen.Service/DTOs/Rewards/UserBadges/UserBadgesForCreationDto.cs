using Tahseen.Domain.Entities;

namespace Tahseen.Service.DTOs.Rewards.UserBadges;

public class UserBadgesForCreationDto
{
    public long UserId {get; set;}
    public long BadgeId {get; set;}
    public string Description { get; set; }
}