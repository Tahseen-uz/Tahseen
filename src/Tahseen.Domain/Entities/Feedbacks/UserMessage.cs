using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Feedbacks;

public class UserMessage:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Messages { get; set; }
}