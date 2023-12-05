using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.Interfaces.IEventsServices;
using Tahseen.Service.DTOs.Events.EventRegistration;

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
        var Check = await this._repository
            .SelectAll()
            .Where(a => a.UserId == dto.UserId && a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (Check != null)
            throw new TahseenException(409, "This EventRegistration is exist");
        
        var eventRegistration = this._mapper.Map<EventRegistration>(dto);
        var result= await this._repository.CreateAsync(eventRegistration);
        return this._mapper.Map<EventRegistrationForResultDto>(result);
    }

    public async Task<EventRegistrationForResultDto> ModifyAsync(long id, EventRegistrationForUpdateDto dto)
    {
        var eventRegistration = await this._repository
            .SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (eventRegistration is not null)
        {
            var mappedEventRegistration = this._mapper.Map(dto, eventRegistration);
            var result = await this._repository.UpdateAsync(mappedEventRegistration);
            result.UpdatedAt = DateTime.UtcNow;
            return this._mapper.Map<EventRegistrationForResultDto>(result);
        }
        throw new Exception("EventRegistration not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var eventRegistration = await this._repository
            .SelectAll()
            .Where(e => e.Id == id && !e.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (eventRegistration is null)
            throw new TahseenException(404, "EventRegistration is not found");

        return await this._repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EventRegistrationForResultDto>> RetrieveAllAsync()
    {
        var AllData = await this._repository
            .SelectAll()
            .Where(e => e.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        var result = this._mapper.Map<IEnumerable<EventRegistrationForResultDto>>(AllData);
        
        foreach(var item in result)
        {
            item.Status = item.Status.ToString();
        }

        return result;
    }

    public async Task<EventRegistrationForResultDto> RetrieveByIdAsync(long id)
    {
        var eventRegistration = await this._repository
            .SelectAll()
            .Where(e => e.Id == id && !e.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if(eventRegistration is null)
            throw new Exception("EventRegistration not found");

        var result =  this._mapper.Map<EventRegistrationForResultDto>(eventRegistration);    
        result.Status = result.Status.ToString();
        return result;
        
    }
}