using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tahseen.Service.DTOs.Events.EventRegistration;

namespace Tahseen.Service.Interfaces.IEventService
{
    public interface IEventRegistrationService
    {
        public Task<bool> RemoveAsync(long Id);
        public IQueryable<EventRegistrationForResultDto> RetrieveAll();
        public Task<EventRegistrationForResultDto> RetrieveByIdAsync(long Id);
        public Task<EventRegistrationForResultDto> AddAsync(EventRegistrationForCreationDto dto);
        public Task<EventRegistrationForResultDto> ModifyAsync(long Id, EventRegistrationForCreationDto dto);
    }
}
