using System.ComponentModel.DataAnnotations;
using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Rewards;

public class UserBadges : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long BadgeId { get; set; }
    public Badge Badge { get; set; }
    public string Description { get; set; }
}
