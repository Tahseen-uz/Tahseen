using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.DTOs.Events.Event;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IEventService;

namespace Tahseen.Service.Services.Events;

public class EventService : IEventService
{
    private readonly IMapper mapper;
    private readonly IRepostory<Event> eventRepository;

    public EventService(IMapper mapper, IRepostory<Event> eventRepository)
    {
        this.mapper = mapper;
        this.eventRepository = eventRepository;
    }
    public async Task<EventForResultDto> CreateAsync(EventForResultDto dto)
    {
        var incident = await this.eventRepository.SelectAll().
            FirstOrDefaultAsync(i => i.Description == dto.Description);

        if (incident is not null)
            throw new TahseenException(409, "Event is already exist");
        var mapperEvent = this.mapper.Map<Event>(dto);
        mapperEvent.CreatedAt = DateTime.UtcNow;

        var result = await this.eventRepository.CreateAsync(mapperEvent);

        return this.mapper.Map<EventForResultDto>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var incident = await this.eventRepository.SelectByIdAsync(id);
        if (incident is null)
            throw new TahseenException(404, "Event is not found");

        await this.eventRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<EventForResultDto>> GetAllAsync()
    {
        var events = await this.eventRepository.SelectAll()
            .ToListAsync();
        return this.mapper.Map<IEnumerable<EventForResultDto>>(events);
    }

    public async Task<EventForResultDto> GetByIdAsync(long id)
    {
        var incident = await this.eventRepository.SelectByIdAsync(id);

        if (incident is null)
            throw new TahseenException(404, "User is already exist");
        return this.mapper.Map<EventForResultDto>(incident);
    }

    public async Task<EventForResultDto> UpdateAsync(EventForResultDto dto)
    {
        var incident = await this.eventRepository.SelectByIdAsync(dto.Id);

        if (incident is null)
            throw new TahseenException(404, "Event is not found");

        var mappedEvent = this.mapper.Map<Event>(dto);
        mappedEvent.UpdatedAt = DateTime.UtcNow;

        return this.mapper.Map<EventForResultDto>(mappedEvent);
    }
}
