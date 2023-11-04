using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.DTOs.Events.EventRegistration;
using Tahseen.Service.DTOs.Events.Events;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IEventsServices;

namespace Tahseen.Service.Services.Events;

public class EventRegistrationService : IEventRegistrationService
{
    private readonly IMapper _mapper;
    private readonly IRepository<EventRegistration> _repository;

    public EventRegistrationService(IMapper mapper, IRepository<EventRegistration> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<EventRegistrationForResultDto> AddAsync(EventRegistrationForCreationDto dto)
    {
        var Check = await this._repository.SelectAll().Where(a => a.UserId == dto.UserId && a.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This EventRegistration is exist");
        }
        var eventRegistration = _mapper.Map<EventRegistration>(dto);
        var result= await _repository.CreateAsync(eventRegistration);
        return _mapper.Map<EventRegistrationForResultDto>(result);
    }

    public async Task<EventRegistrationForResultDto> ModifyAsync(long id, EventRegistrationForUpdateDto dto)
    {
        var eventRegistration = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (eventRegistration is not null)
        {
            var mappedEventRegistration = _mapper.Map(dto, eventRegistration);
            var result = await _repository.UpdateAsync(mappedEventRegistration);
            result.UpdatedAt = DateTime.UtcNow;
            return _mapper.Map<EventRegistrationForResultDto>(result);
        }
        throw new Exception("EventRegistration not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EventRegistrationForResultDto>> RetrieveAllAsync()
    {
        var AllData = this._repository.SelectAll().Where(e => e.IsDeleted == false);
        return this._mapper.Map<IEnumerable<EventRegistrationForResultDto>>(AllData);
    }

    public async Task<EventRegistrationForResultDto> RetrieveByIdAsync(long id)
    {
        var eventRegistration = await _repository.SelectByIdAsync(id);
        if (eventRegistration is not null && !eventRegistration.IsDeleted)
            return _mapper.Map<EventRegistrationForResultDto>(eventRegistration);
        
        throw new Exception("EventRegistration not found");
    }
}