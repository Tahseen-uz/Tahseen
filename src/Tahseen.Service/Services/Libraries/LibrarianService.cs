using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Data.Repositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Librarians;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Librarians;
using Tahseen.Service.DTOs.Users.ChangePassword;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Service.Helpers;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.ILibrariansService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tahseen.Service.Services.Libraries;

public class LibrarianService : ILibrarianService
{
    private readonly IRepository<Librarian> repository;
    private readonly IMapper mapper;
    private readonly IFileUploadService _fileUploadService;
    public LibrarianService(IRepository<Librarian> repository, IMapper mapper, IFileUploadService fileUploadService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this._fileUploadService = fileUploadService;
    }

    public async Task<LibrarianForResultDto> AddAsync(LibrarianForCreationDto dto)
    {
        var Check = await this.repository.SelectAll().Where(a => a.FirstName == dto.FirstName && a.LastName == dto.LastName && a.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This Librarian is exist");
        }
        var mappedLibrarian = mapper.Map<Librarian>(dto);

        if(dto.Image != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "LibrarianAssets",
                FormFile = dto.Image,
            };
            var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);
            mappedLibrarian.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        }


        mappedLibrarian.Roles = Domain.Enums.Roles.Librarian;
        var HashedPassword = PasswordHelper.Hash(dto.Password);
        mappedLibrarian.Password = HashedPassword.Hash;
        mappedLibrarian.Salt = HashedPassword.Salt;
        var result = await this.repository.CreateAsync(mappedLibrarian);
        return this.mapper.Map<LibrarianForResultDto>(result);
    }

    public async Task<LibrarianForResultDto> ModifyAsync(long id, LibrarianForUpdateDto dto)
    {
        var librarian = await this.repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (librarian == null)
            throw new TahseenException(404, "Librarian not found");
        if(dto != null && dto.Image != null)
        {
            if(dto.Image != null)
            {
                await _fileUploadService.FileDeleteAsync(librarian.Image);
            }
            var FileUploadForCreation = new FileUploadForCreationDto()
            {
                FolderPath = "LibrarianAssets",
                FormFile = dto.Image,
            };
            var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

            librarian.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            librarian.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : librarian.FirstName;
            librarian.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : librarian.LastName;
            librarian.PhoneNumber = !string.IsNullOrEmpty(dto.PhoneNumber) ? dto.PhoneNumber : librarian.PhoneNumber;
            librarian.DateOfBirth = !string.IsNullOrEmpty(dto.DateOfBirth) ? dto.DateOfBirth : librarian.DateOfBirth; 
        }
        if(dto != null && dto.Image == null)
        {
            librarian.Image = librarian.Image;
            librarian.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : librarian.FirstName;
            librarian.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : librarian.LastName;
            librarian.PhoneNumber = !string.IsNullOrEmpty(dto.PhoneNumber) ? dto.PhoneNumber : librarian.PhoneNumber;
            librarian.DateOfBirth = !string.IsNullOrEmpty(dto.DateOfBirth) ? dto.DateOfBirth : librarian.DateOfBirth;
        }

        librarian.UpdatedAt = DateTime.UtcNow;

        var result = await repository.UpdateAsync(librarian);
        return mapper.Map<LibrarianForResultDto>(result);
        
    }

    public async Task<bool> ChangePasswordAsync(long Id, LibrarianForChangePasswordDto dto)
    {
        var data = await repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (data == null || PasswordHelper.Verify(dto.OldPassword, data.Salt, data.Password) == false)
        {
            throw new TahseenException(400, "User or Password is Incorrect");
        }
        else if (dto.NewPassword != dto.ConfirmPassword)
        {
            throw new TahseenException(400, "New Password and Confirm Password does not Match");
        }
        var HashedPassword = PasswordHelper.Hash(dto.ConfirmPassword);
        data.Salt = HashedPassword.Salt;
        data.Password = HashedPassword.Hash;
        await repository.UpdateAsync(data);
        return true;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var librarian = await this.repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (librarian == null)
            throw new TahseenException(404, "Lirarian not found");

        if(librarian.Image != null)
        {
            await _fileUploadService.FileDeleteAsync(librarian.Image);
        }
        return await this.repository.DeleteAsync(librarian.Id);
    }

    public async Task<IEnumerable<LibrarianForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var librarians = await this.repository.SelectAll().
            Where(l => !l.IsDeleted)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        var result = this.mapper.Map<IEnumerable<LibrarianForResultDto>>(librarians);
        foreach (var item in result)
        {
            item.Roles = item.Roles.ToString();
        }
        return result;
    }

    public async Task<LibrarianForResultDto> RetrieveByIdAsync(long id)
    {
        var librarian = await this.repository.SelectByIdAsync(id);
        if (librarian is null || librarian.IsDeleted)
            throw new TahseenException(404, "Librarian not found");

        var result = this.mapper.Map<LibrarianForResultDto>(librarian);
        result.Roles = result.Roles.ToString();
        return result;

    }
}