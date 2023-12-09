
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Events.Events;

namespace Tahseen.Service.Interfaces.IEventsServices
{
    public interface IEventsService
    {
        public Task<EventForResultDto> AddAsync(EventForCreationDto dto);
        public Task<EventForResultDto> ModifyAsync(long Id, EventForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<EventForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<EventForResultDto>> RetrieveAllAsync(PaginationParams @params);
    }
}
