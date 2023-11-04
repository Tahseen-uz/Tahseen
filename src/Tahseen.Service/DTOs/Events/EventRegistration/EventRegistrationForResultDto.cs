using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Events.EventRegistration;

public class EventRegistrationForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long EventId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public EventRegistrationStatus Status { get; set; }
}