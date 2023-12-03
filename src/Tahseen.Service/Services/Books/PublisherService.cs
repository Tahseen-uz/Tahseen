using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.Books;

public class PublisherService : IPublisherService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Publisher> _repository;
    private readonly IFileUploadService _fileUploadService;
    public PublisherService(IMapper mapper, 
        IRepository<Publisher> repository, 
        IFileUploadService fileUploadService)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._fileUploadService = fileUploadService;
    }

    public async Task<PublisherForResultDto> AddAsync(PublisherForCreationDto dto)
    {
        var Check = await this._repository.SelectAll()
            .Where(a => a.Name == dto.Name && a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (Check != null)
            throw new TahseenException(409, "This Publisher is exist");

        var FileUploadForCreation = new FileUploadForCreationDto()
        {
            FolderPath = "PublisherImages",
            FormFile = dto.Image,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = this._mapper.Map<Publisher>(dto);
        mapped.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        var result = await this._repository.CreateAsync(mapped);

        return _mapper.Map<PublisherForResultDto>(result);
    }

    public async Task<PublisherForResultDto> RetrieveByIdAsync(long id)
    {
        var publisher = await this._repository.SelectAll()
            .Where(p => p.Id == id && !p.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (publisher is null)
            throw new TahseenException(404, "Publisher is not found");

        return this._mapper.Map<PublisherForResultDto>(publisher);
    }

    public async Task<PublisherForResultDto> ModifyAsync(long id, PublisherForUpdateDto dto)
    {
        var publisher = await this._repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (publisher == null)
            throw new TahseenException(404, "Publisher doesn't found");

        await this._fileUploadService.FileDeleteAsync(publisher.Image);

        var FileUploadForCreation = new FileUploadForCreationDto()
        {
            FolderPath = "PublisherImages",
            FormFile = dto.Image,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var publisherMapped = this._mapper.Map(dto, publisher);
        publisherMapped.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        publisherMapped.UpdatedAt = DateTime.UtcNow;
        var result = await this._repository.UpdateAsync(publisherMapped);

        return _mapper.Map<PublisherForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var publisher = await this._repository.SelectAll()
            .Where(p => p.Id == id && p.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (publisher is null)
            throw new TahseenException(404, "Publisher is not found");

        await this._fileUploadService.FileDeleteAsync(publisher.Image);

        return await this._repository.DeleteAsync(id);
    }
   
    public async Task<IEnumerable<PublisherForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var results = await this._repository.SelectAll()
            .Where(t => !t.IsDeleted)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return this._mapper.Map<IEnumerable<PublisherForResultDto>>(results);
    }
}
