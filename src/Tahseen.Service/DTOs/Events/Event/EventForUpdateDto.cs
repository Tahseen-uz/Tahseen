using System;
using System.Linq;
using System.Text;
using Tahseen.Domain.Enums;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Tahseen.Service.DTOs.Events.Event
{
    public class EventForUpdateDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime EndDate { get; set; }
        public long Participants { get; set; }
        public DateTime StartDate { get; set; }
        public EventStatus Status { get; set; }
        public string Description { get; set; }
    }
}
