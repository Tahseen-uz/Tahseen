using Tahseen.Service.DTOs.Events.EventRegistration;

namespace Tahseen.Service.Interfaces.IEventService;

public interface IEventRegistrationService
{
    public Task<EventRegistrationForResultDto> CreatedAsync(EventRegistrationForCreationDto dto);
    public Task<EventRegistrationForResultDto> UpdatedAsync(EventRegistrationForUpdateDto dto);
    public Task<bool>  DeleteAsync(long id);
    public Task<EventRegistrationForResultDto> GetByIdAsync(long id);
    public Task<IEnumerable<EventRegistrationForResultDto>> GetAllAsync();
}
