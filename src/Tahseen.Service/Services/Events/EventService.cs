using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Events;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Events.Events;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IEventsServices;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.Events;

public class EventService : IEventsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Event> _repository;
    private readonly IFileUploadService _fileUploadService;


    public EventService(IMapper mapper, IRepository<Event> repository, IFileUploadService fileUploadService)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._fileUploadService = fileUploadService;
    }

    public async Task<EventForResultDto> AddAsync(EventForCreationDto dto)
    {
        var Check = await this._repository.SelectAll().Where(a => a.Title == dto.Title && a.Location == dto.Location && a.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This Event is exist");
        }
        var FileUploadForCreation = new FileUploadForCreationDto()
        {
            FolderPath = "EventImages",
            FormFile = dto.Image,
        };
        var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

        var MappedData = this._mapper.Map<Event>(dto);

        MappedData.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        var result = await _repository.CreateAsync(MappedData);

        return _mapper.Map<EventForResultDto>(result);
        }

    public async Task<EventForResultDto> ModifyAsync(long id, EventForUpdateDto dto)
    {
        var @event = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (@event is not null)
        {
            //Deleting Image
            await _fileUploadService.FileDeleteAsync(@event.Image);

            //Uploading Image
            var FileUploadForCreation = new FileUploadForCreationDto()
            {
                FolderPath = "EventImages",
                FormFile = dto.Image,
            };
            var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

            var MappedData = this._mapper.Map(dto, @event);
            MappedData.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            MappedData.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(MappedData);
            return _mapper.Map<EventForResultDto>(MappedData);
        }
        throw new Exception("Event not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var results = await this._repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if(results is null)
        {
            throw new TahseenException(404, "Not found");
        }
        await _fileUploadService.FileDeleteAsync(results.Image);
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EventForResultDto>> RetrieveAllAsync()
    {
        var results = this._repository.SelectAll().Where(a => !a.IsDeleted);
        foreach (var result in results)
        {
            result.Image = $"https://localhost:7020/{result.Image.Replace('\\', '/').TrimStart('/')}";
        }
        return _mapper.Map<IEnumerable<EventForResultDto>>(results);
    }

    public async Task<EventForResultDto> RetrieveByIdAsync(long id)
    {
        var result = await _repository.SelectByIdAsync(id);
        if (result is not null && !result.IsDeleted)
        {
            result.Image = $"https://localhost:7020/{result .Image.Replace('\\', '/').TrimStart('/')}";
            return _mapper.Map<EventForResultDto>(result);
        }

        throw new TahseenException(404, "Event not found");
    }


}