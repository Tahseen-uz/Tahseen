using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.DTOs.Events.EventRegistration;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IEventService;

namespace Tahseen.Service.Services.Events;

public class EventRegistrationService : IEventRegistrationService
{
    private readonly IRepostory<EventRegistration> eventRegistrationRepository;
    private readonly IMapper mapper;

    public EventRegistrationService(IMapper mapper, IRepostory<EventRegistration> eventRegistrationRepository)
    {
        this.mapper = mapper;
        this.eventRegistrationRepository = eventRegistrationRepository;
    }
    public async Task<EventRegistrationForResultDto> CreatedAsync(EventRegistrationForCreationDto dto)
    {
        var incidentRegistration =  await this.eventRegistrationRepository.SelectAll()
            .FirstOrDefaultAsync(i => i.EventId == dto.EventId && i.UserId == dto.UserId);
        if (incidentRegistration is not null)
            throw new TahseenException(409, "EventRegistration is already exist");
        var mapped = this.mapper.Map<EventRegistration>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        var result = await this.eventRegistrationRepository.CreateAsync(mapped);

        return this.mapper.Map<EventRegistrationForResultDto>(result);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var result = await this.eventRegistrationRepository.SelectByIdAsync(id);

        if (result is null)
            throw new TahseenException(404, "EventRegistration is not found");

        await this.eventRegistrationRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<EventRegistrationForResultDto>> GetAllAsync()
    {
        var eventRegistrations = await this.eventRegistrationRepository.SelectAll()
            .ToListAsync();

        return this.mapper.Map<IEnumerable<EventRegistrationForResultDto>>(eventRegistrations);
    }

    public async Task<EventRegistrationForResultDto> GetByIdAsync(long id)
    {
        var result = await this.eventRegistrationRepository.SelectByIdAsync(id);
        if (result is null)
            throw new TahseenException(404, "EventRegistration is not found");

        return this.mapper.Map<EventRegistrationForResultDto>(result);
    }

    public async Task<EventRegistrationForResultDto> UpdatedAsync(EventRegistrationForUpdateDto dto)
    {
        var result = await this.eventRegistrationRepository.SelectByIdAsync(dto.Id);
        if (result is null)
            throw new TahseenException(404, "EventRegistration is not found");

        var mapped = this.mapper.Map<EventRegistration>(dto);
        mapped.UpdatedAt = DateTime.UtcNow;

        return this.mapper.Map<EventRegistrationForResultDto>(mapped);
    }
}
