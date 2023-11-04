using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Events.EventRegistration;

public class EventRegistrationForCreationDto
{
    public long UserId { get; set; }
    public long EventId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public EventRegistrationStatus Status { get; set; }
}