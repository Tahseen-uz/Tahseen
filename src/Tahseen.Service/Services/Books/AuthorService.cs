using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.Books;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Author> _repository;
    private readonly IFileUploadService _fileUploadService;

    public AuthorService(
        IMapper mapper,
        IRepository<Author> repository,
        IFileUploadService fileUploadService)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._fileUploadService = fileUploadService;
    }

    public async Task<AuthorForResultDto> AddAsync(AuthorForCreationDto dto)
    {
        var check = await this._repository.SelectAll()
            .Where(a => a.FirstName == dto.FirstName && a.LastName == dto.LastName && !a.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (check != null)
            throw new TahseenException(409, "This Author already exists.");

        //Uploading Image
        var FileUploadForCreation = new FileUploadForCreationDto()
        {
            FolderPath = "AuthorAssets",
            FormFile = dto.AuthorImage,
        };
        var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

        var MappedData = this._mapper.Map<Author>(dto);
        MappedData.AuthorImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        var result = await _repository.CreateAsync(MappedData);

        return _mapper.Map<AuthorForResultDto>(result);
    }

    public async Task<AuthorForResultDto> ModifyAsync(long id, AuthorForUpdateDto dto)
    {
        var author = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (author is not null)
        {
            //Deleting Image
            await _fileUploadService.FileDeleteAsync(author.AuthorImage);
            
            //Uploading Image
            var FileUploadForCreation = new FileUploadForCreationDto()
            {
                FolderPath = "AuthorImages",        
                FormFile = dto.AuthorImage,
            };
            var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

            var MappedData = this._mapper.Map(dto, author);
            MappedData.AuthorImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            MappedData.UpdatedAt = DateTime.UtcNow;

            var result = await _repository.UpdateAsync(MappedData);
            return _mapper.Map<AuthorForResultDto>(result);
        }
        throw new Exception("Author not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var results = await this._repository.SelectAll()
            .Where(a => a.Id == id && !a.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (results is null)
            throw new TahseenException(404, "Author is not Found");

        await this._fileUploadService.FileDeleteAsync(results.AuthorImage);
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AuthorForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var results = await this._repository.SelectAll()
            .Where(a => !a.IsDeleted)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        var mappedData = _mapper.Map<IEnumerable<AuthorForResultDto>>(results);
        foreach (var item in mappedData)
        {
            item.Nationality = item.Nationality.ToString();
        }
        return mappedData;
    }

    public async Task<AuthorForResultDto> RetrieveByIdAsync(long id)
    {
        var author = await this._repository.SelectAll()
            .Where(a => a.Id == id && !a.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if(author is null)
            throw new TahseenException(404, "Author is not found");
        
        return this._mapper.Map<AuthorForResultDto>(author);
    }
}