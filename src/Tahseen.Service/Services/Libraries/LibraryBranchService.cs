using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.ILibrariesService;

namespace Tahseen.Service.Services.Libraries;

public class LibraryBranchService : ILibraryBranchService
{
    private readonly IMapper mapper;
    private readonly IRepository<LibraryBranch> repository;
    private readonly IFileUploadService fileUploadService;
    public LibraryBranchService(IMapper mapper, IRepository<LibraryBranch> repository, IFileUploadService fileUploadService)
    {
        this.mapper = mapper;
        this.repository = repository;
        this.fileUploadService = fileUploadService;
    }

    public async Task<LibraryBranchForResultDto> AddAsync(LibraryBranchForCreationDto dto)
    {
        var Check = await this.repository.SelectAll().Where(a => a.Name == dto.Name && a.Address == dto.Address && a.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This LibraryBranch is exist");
        }

        var mappedLibraryBranch = this.mapper.Map<LibraryBranch>(dto);
        if(dto.Image != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "LibraryBranchAssets",
                FormFile = dto.Image
            };
            var FileResult = await fileUploadService.FileUploadAsync(FileUploadForCreation);
            mappedLibraryBranch.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        }

        var result = await this.repository.CreateAsync(mappedLibraryBranch);
        return this.mapper.Map<LibraryBranchForResultDto>(result);
    }

    public async Task<LibraryBranchForResultDto> ModifyAsync(long id, LibraryBranchForUpdateDto dto)
    {
        var libraryBranch = await this.repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (libraryBranch == null)
            throw new TahseenException(404, "LibraryBranch not found");

        if(dto != null && dto.Image != null)
        {
            if(dto.Image != null)
            {
                await fileUploadService.FileDeleteAsync(libraryBranch.Image);
            }
            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "LibraryBranchAssets",
                FormFile = dto.Image
            };

            var FileResult = await fileUploadService.FileUploadAsync(FileUploadForCreation);

            libraryBranch.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            libraryBranch.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : libraryBranch.Name;
            libraryBranch.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : libraryBranch.Address;
            libraryBranch.PhoneNumber = !string.IsNullOrEmpty(dto.PhoneNumber) ? dto.PhoneNumber : libraryBranch.PhoneNumber;
            libraryBranch.OpeningHours = !string.IsNullOrEmpty(dto.OpeningHours) ? dto.OpeningHours : libraryBranch.OpeningHours;
            libraryBranch.LibraryType = dto.LibraryType != null ? dto.LibraryType : libraryBranch.LibraryType;
        }
        if (dto != null && dto.Image == null)
        {
            libraryBranch.Image = libraryBranch.Image;
            libraryBranch.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : libraryBranch.Name;
            libraryBranch.Address = !string.IsNullOrEmpty(dto.Address) ? dto.Address : libraryBranch.Address;
            libraryBranch.PhoneNumber = !string.IsNullOrEmpty(dto.PhoneNumber) ? dto.PhoneNumber : libraryBranch.PhoneNumber;
            libraryBranch.OpeningHours = !string.IsNullOrEmpty(dto.OpeningHours) ? dto.OpeningHours : libraryBranch.OpeningHours;
            libraryBranch.LibraryType = dto.LibraryType != null ? dto.LibraryType : libraryBranch.LibraryType;
        }

        libraryBranch.UpdatedAt = DateTime.UtcNow;

        var result = await this.repository.UpdateAsync(libraryBranch);
        return this.mapper.Map<LibraryBranchForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var libraryBranch = await this.repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (libraryBranch == null)
            throw new TahseenException(404, "LibraryBranch not found");
        if (libraryBranch.Image != null)
            await fileUploadService.FileDeleteAsync(libraryBranch.Image);

        return await this.repository.DeleteAsync(libraryBranch.Id);
    }

    public async Task<IEnumerable<LibraryBranchForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var result = await this.repository.SelectAll()
            .Where(l => !l.IsDeleted)
            .Include(l => l.Librarians)
            .Include(b => b.TotalBooks)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

       
        return this.mapper.Map<IEnumerable<LibraryBranchForResultDto>>(result);
    }

    public async Task<LibraryBranchForResultDto> RetrieveByIdAsync(long id)
    {
        var libraryBranch = await this.repository.SelectAll()
            .Where(l => !l.IsDeleted)
            .Include(l => l.Librarians)
            .Include(b => b.TotalBooks)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (libraryBranch == null || libraryBranch.IsDeleted)
            throw new TahseenException(404, "LibraryBranch not found");

        return mapper.Map<LibraryBranchForResultDto>(libraryBranch);
    }
}
