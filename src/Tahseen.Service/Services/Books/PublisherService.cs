using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.Interfaces.IBookServices;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.Configurations;
using Tahseen.Service.Extensions;

namespace Tahseen.Service.Services.Books;

public class PublisherService : IPublisherService
{
    private readonly IMapper mapper;
    private readonly IRepository<Publisher> repository;
    private readonly IFileUploadService fileUploadService;
    public PublisherService(IMapper mapper, IRepository<Publisher> repository, IFileUploadService fileUploadService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.fileUploadService = fileUploadService;
    }

    public async Task<PublisherForResultDto> AddAsync(PublisherForCreationDto dto)
    {
        var Check = await this.repository.SelectAll().Where(a => a.Name == dto.Name && a.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This Publisher is exist");
        }
        var mapped = mapper.Map<Publisher>(dto);

        if (dto.Image != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto()
            {
                FolderPath = "PublisherImages",
                FormFile = dto.Image,
            };
            var FileResult = await fileUploadService.FileUploadAsync(FileUploadForCreation);
            mapped.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);

        }

        var result = await this.repository.CreateAsync(mapped);

        return mapper.Map<PublisherForResultDto>(result);
    }

    public async Task<PublisherForResultDto> RetrieveByIdAsync(long id)
    {
        var publisher = await this.repository.SelectByIdAsync(id);
        if (publisher == null || publisher.IsDeleted)
            throw new TahseenException(404, "Publisher doesn't found");

        return mapper.Map<PublisherForResultDto>(publisher);
    }

    public async Task<PublisherForResultDto> ModifyAsync(long id, PublisherForUpdateDto dto)
    {
        var publisher = await this.repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (publisher == null)
            throw new TahseenException(404, "Publisher doesn't found");

        if(dto != null && dto.Image != null)
        {
            if(publisher.Image != null)
            {
                await this.fileUploadService.FileDeleteAsync(publisher.Image);
            }

            var FileUploadForCreation = new FileUploadForCreationDto()
            {
                FolderPath = "PublisherImages",
                FormFile = dto.Image,
            };
            var FileResult = await fileUploadService.FileUploadAsync(FileUploadForCreation);

            publisher.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            publisher.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : publisher.Name;
            publisher.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : publisher.Address;
            publisher.ContactInformation = !string.IsNullOrEmpty(dto.ContactInformation) ? dto.ContactInformation : publisher.ContactInformation;
        }
        if(dto != null && dto.Image == null)
        {
            publisher.Image = publisher.Image;
            publisher.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : publisher.Name;
            publisher.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : publisher.Address;
            publisher.ContactInformation = !string.IsNullOrEmpty(dto.ContactInformation) ? dto.ContactInformation : publisher.ContactInformation;
        }

        publisher.UpdatedAt = DateTime.UtcNow;
        var result = await this.repository.UpdateAsync(publisher);

        return mapper.Map<PublisherForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var publisher = await repository.SelectAll()
            .Where(p => p.Id == id && p.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (publisher is null)
            throw new TahseenException(404, "Publisher is not found");

        if (publisher.Image != null)
        {
            await fileUploadService.FileDeleteAsync(publisher.Image);
        }

        return await repository.DeleteAsync(id);
    }
   
    public async Task<IEnumerable<PublisherForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var results = await this.repository.SelectAll()
            .Where(t => !t.IsDeleted)
            .ToPagedList(@params)
            .ToListAsync();

        return mapper.Map<IEnumerable<PublisherForResultDto>>(results);
    }
}
