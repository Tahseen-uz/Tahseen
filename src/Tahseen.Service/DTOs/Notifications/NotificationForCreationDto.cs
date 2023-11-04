using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Notifications;

public class NotificationForCreationDto
{
    public long UserId { get; set; }
    public string Message { get; set; }
    public DateTime NotificationSentTime { get; set; } = DateTime.UtcNow;
    public NotificationStatus NotificationStatus { get; set; }
}