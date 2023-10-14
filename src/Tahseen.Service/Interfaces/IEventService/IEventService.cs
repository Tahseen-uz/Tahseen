using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tahseen.Service.DTOs.Events.Event;

namespace Tahseen.Service.Interfaces.IEventService
{
    public interface IEventService
    {
        public Task<bool> RemoveAsync(long Id);
        public IQueryable<EventForResultDto> RetrieveAll();
        public Task<EventForResultDto> RetrieveByIdAsync(long Id);
        public Task<EventForResultDto> AddAsync(EventForCreationDto dto);
        public Task<EventForResultDto> ModifyAsync(long Id, EventForUpdateDto dto);
    }
}
