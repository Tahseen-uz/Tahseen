using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Events.Events;

public class EventForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public string Location { get; set; }
    public string Image { get; set; }
    public long Participants { get; set; }
    public DateTime StartDate { get; set; }  // Change to DateTimeOffset
    public DateTime EndDate { get; set; }
    public EventStatus Status { get; set; }
}