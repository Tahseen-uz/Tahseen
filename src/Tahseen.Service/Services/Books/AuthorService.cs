using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Enums;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Service.Helpers;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.Interfaces.IFileUploadService;
using File = System.IO.File;

namespace Tahseen.Service.Services.Books;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Author> _repository;
    private readonly IFileUploadService _fileUploadService;

    public AuthorService(IMapper mapper, IRepository<Author> repository, IFileUploadService fileUploadService)
    {
        this._mapper = mapper;
        this._repository = repository;
        _fileUploadService = fileUploadService;
    }

    public async Task<AuthorForResultDto> AddAsync(AuthorForCreationDto dto)
    {
        var check = await this._repository.SelectAll()
            .Where(a => a.FirstName == dto.FirstName && a.LastName == dto.LastName && !a.IsDeleted)
            .FirstOrDefaultAsync();

        if (check != null)
            throw new TahseenException(409, "This Author already exists.");

        var MappedData = this._mapper.Map<Author>(dto);
        if (dto.AuthorImage != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto()
            {
                FolderPath = "AuthorAssets",
                FormFile = dto.AuthorImage,
            };
            var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);
            MappedData.AuthorImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);

        }

        var result = await _repository.CreateAsync(MappedData);

        return _mapper.Map<AuthorForResultDto>(result);
    }



    public async Task<AuthorForResultDto> ModifyAsync(long id, AuthorForUpdateDto dto)
    {
        var author = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (author is not null)
        {
            if (dto != null && dto.AuthorImage != null)
            {
                if (author.AuthorImage != null)
                {
                    await _fileUploadService.FileDeleteAsync(author.AuthorImage);
                }
                var FileUploadForCreation = new FileUploadForCreationDto()
                {
                    FolderPath = "AuthorImages",
                    FormFile = dto.AuthorImage,
                };
                var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

                author.AuthorImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
                author.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : author.FirstName;
                author.LastName = !string.IsNullOrEmpty(dto.LastName)  ? dto.LastName : author.LastName;
                author.Biography = !string.IsNullOrEmpty(dto.Biography) ? dto.Biography : author.Biography;
                author.Nationality = dto.Nationality != null ? dto.Nationality : author.Nationality;
            }
            
            if (dto != null && dto.AuthorImage == null) 
            {
                author.AuthorImage = author.AuthorImage;
                author.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : author.FirstName;
                author.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : author.LastName;
                author.Biography = !string.IsNullOrEmpty(dto.Biography) ? dto.Biography : author.Biography;
                author.Nationality = dto.Nationality != null ? dto.Nationality : author.Nationality;
            }
            
            author.UpdatedAt = DateTime.UtcNow;

            var result = await _repository.UpdateAsync(author);
            return _mapper.Map<AuthorForResultDto>(result);
        }
        throw new Exception("Author not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var results = await this._repository.SelectAll().Where(a => a.Id == id && !a.IsDeleted).FirstOrDefaultAsync();
        if (results is null)
            throw new TahseenException(404, "Author is not Found");
        if (results.AuthorImage != null)
        {
            await _fileUploadService.FileDeleteAsync(results.AuthorImage);
        }
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
        var author = await _repository.SelectByIdAsync(id);
        if (author is not null && !author.IsDeleted)
        {
            return _mapper.Map<AuthorForResultDto>(author);
        }

        throw new TahseenException(404, "Author not found");
    }


}