using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Events.Event;

public class EventForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public long Participants { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public EventStatus Status { get; set; }
}
