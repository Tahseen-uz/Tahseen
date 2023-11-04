using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Enums;
using System;

namespace Tahseen.Service.DTOs.Events.Events
{
    public class EventForCreationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public IFormFile Image { get; set; }
        public long Participants { get; set; }
        public DateTime StartDate { get; set; }  // Change to DateTimeOffset
        public DateTime EndDate { get; set; }    // Change to DateTimeOffset
        public EventStatus Status { get; set; }
    }
}