using Tahseen.Domain.Enums;
using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Events;

public class EventRegistration : Auditable
{
    public User User { get; set; }
    public long UserId { get; set; }
    public Event Event { get; set; }
    public long EventId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public EventRegistrationStatus Status { get; set; }
}
