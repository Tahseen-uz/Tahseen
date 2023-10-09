﻿using Tahseen.Domain.Enums.Events;

namespace Tahseen.Domain.Entities.Events
{
    public class Event
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Location { get; set; }    
        public long Participants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EventStatus Status { get; set; }
    }
}