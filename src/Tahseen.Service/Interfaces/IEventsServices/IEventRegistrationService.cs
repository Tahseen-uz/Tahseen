using Tahseen.Service.DTOs.Events.EventRegistration;

namespace Tahseen.Service.Interfaces.IEventsServices
{
    public interface IEventRegistrationService
    {
        public Task<EventRegistrationForResultDto> AddAsync(EventRegistrationForCreationDto dto);
        public Task<EventRegistrationForResultDto> ModifyAsync(long Id, EventRegistrationForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<EventRegistrationForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<EventRegistrationForResultDto>> RetrieveAllAsync();

    }
}
