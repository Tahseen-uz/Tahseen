using Tahseen.Domain.Commons;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities.Events
{
    public class Event:Auditable
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Location { get; set; }  
        public string Image { get; set; }
        public long Participants { get; set; }
        public DateTime StartDate { get; set; }  // Change to DateTimeOffset
        public DateTime EndDate { get; set; }
        public EventStatus Status { get; set; }

        public IEnumerable<EventRegistration> EventRegistration { get; set; }
    }
}
