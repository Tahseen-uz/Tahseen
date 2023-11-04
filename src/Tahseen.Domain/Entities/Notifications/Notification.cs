using Tahseen.Domain.Enums;
using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Notifications;

public class Notification:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
    public DateTime NotificationSentTime { get; set; }
    public NotificationStatus NotificationStatus { get; set; }
}