using System;
using System.Linq;
using System.Text;
using Tahseen.Domain.Enums;
using System.Threading.Tasks;
using Tahseen.Domain.Entities;
using System.Collections.Generic;

namespace Tahseen.Service.DTOs.Events.EventRegistration
{
    public class EventRegistrationForCreationDto
    {
        public long UserId { get; set; }
        public long EventId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public EventRegistrationStatus Status { get; set; }
    }
}
