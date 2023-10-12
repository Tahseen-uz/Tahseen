using Tahseen.Service.DTOs.Events.Event;

namespace Tahseen.Service.Interfaces.IEventService;

public interface IEventService
{
    public Task<EventForResultDto> CreateAsync(EventForResultDto dto);
    public Task<EventForResultDto> UpdateAsync(EventForResultDto dto);
    public Task<bool> DeleteAsync(long id);
    public Task<EventForResultDto> GetByIdAsync(long id);
    public Task<IEnumerable<EventForResultDto>> GetAllAsync();
}
