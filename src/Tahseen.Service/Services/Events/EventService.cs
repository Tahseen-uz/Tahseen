using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Events.Events;
using Tahseen.Service.Interfaces.IEventsServices;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.Events;

public class EventService : IEventsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Event> _repository;
    private readonly IFileUploadService _fileUploadService;


    public EventService(IMapper mapper, 
        IRepository<Event> repository, 
        IFileUploadService fileUploadService)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._fileUploadService = fileUploadService;
    }

    public async Task<EventForResultDto> AddAsync(EventForCreationDto dto)
    {
        var Check = await this._repository
            .SelectAll()
            .Where(a => a.Title.ToLower() == dto.Title.ToLower() && a.Location == dto.Location && a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This Event is exist");
        }
        var MappedData = this._mapper.Map<Event>(dto);

        if (dto.Image != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto()
            {
                FolderPath = "EventImages",
                FormFile = dto.Image,
            };
            var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

            MappedData.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        }

        var result = await _repository.CreateAsync(MappedData);

        return _mapper.Map<EventForResultDto>(result);
        }

    public async Task<EventForResultDto> ModifyAsync(long id, EventForUpdateDto dto)
    {
        var @event = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (@event is not null)
        {
            if(dto != null && dto.Image != null)
            {
                if (dto.Image != null)
                {
                    await _fileUploadService.FileDeleteAsync(@event.Image);
                }
                var FileUploadForCreation = new FileUploadForCreationDto()
                {
                    FolderPath = "EventImages",
                    FormFile = dto.Image,
                };
                var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

                @event.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
                @event.Title = !string.IsNullOrEmpty(dto.Title) ? dto.Title : @event.Title;
                @event.Description = !string.IsNullOrEmpty(dto.Description) ? dto.Description : @event.Description;
                @event.Location = !string.IsNullOrEmpty(dto.Location) ? dto.Location : @event.Location;
                @event.Participants = dto.Participants != null ? dto.Participants : @event.Participants;
                @event.StartDate = dto.StartDate != null ? dto.StartDate : @event.StartDate;
                @event.EndDate = dto.EndDate != null ? dto.EndDate : @event.EndDate;
                @event.Status = dto.Status != null ? dto.Status : @event.Status;
            }

            if (dto !=null && dto.Image == null)
            {
                @event.Image = @event.Image;
                @event.Title = !string.IsNullOrEmpty(dto.Title) ? dto.Title : @event.Title;
                @event.Description = !string.IsNullOrEmpty(dto.Description) ? dto.Description : @event.Description;
                @event.Location = !string.IsNullOrEmpty(dto.Location) ? dto.Location : @event.Location;
                @event.Participants = dto.Participants != null ? dto.Participants : @event.Participants;
                @event.StartDate = dto.StartDate != null ? dto.StartDate : @event.StartDate;
                @event.EndDate = dto.EndDate != null ? dto.EndDate : @event.EndDate;
                @event.Status = dto.Status != null ? dto.Status : @event.Status;
            }

            @event.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(@event);
            return _mapper.Map<EventForResultDto>(@event);
        }
        throw new Exception("Event not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var results = await this._repository
            .SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if(results is null)
        {
            throw new TahseenException(404, "Not found");
        }
        if (results.Image != null)
        {
            await _fileUploadService.FileDeleteAsync(results.Image);
        }
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EventForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var results = await this._repository.SelectAll()
            .Where(a => !a.IsDeleted)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        var result = this._mapper.Map<IEnumerable<EventForResultDto>>(results);
        foreach(var item in result)
        {
            item.Status = item.Status.ToString();
        }
        return result;
    }

    public async Task<EventForResultDto> RetrieveByIdAsync(long id)
    {
        var result = await _repository.SelectByIdAsync(id);
        if (result is not null && !result.IsDeleted)
        {
            var res =  this._mapper.Map<EventForResultDto>(result);
            res.Status = res.Status.ToString();
            return res;
        }

        throw new TahseenException(404, "Event not found");
    }


}