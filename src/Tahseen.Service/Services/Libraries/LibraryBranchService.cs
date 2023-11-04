using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;
using Tahseen.Service.Exceptions;
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

        // upload image
        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "LibraryBranchAssets",
            FormFile = dto.Image
        };

        var FileResult = await fileUploadService.FileUploadAsync(FileUploadForCreation);
        var mappedLibraryBranch = this.mapper.Map<LibraryBranch>(dto);
        mappedLibraryBranch.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        var result = await this.repository.CreateAsync(mappedLibraryBranch);
        return this.mapper.Map<LibraryBranchForResultDto>(result);
    }

    public async Task<LibraryBranchForResultDto> ModifyAsync(long id, LibraryBranchForUpdateDto dto)
    {
        var libraryBranch = await this.repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (libraryBranch == null)
            throw new TahseenException(404, "LibraryBranch not found");

        //delete image
        await fileUploadService.FileDeleteAsync(libraryBranch.Image);

        //upload image
        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "LibraryBranchAssets",
            FormFile = dto.Image
        };

        var FileResult = await fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mappedLibraryBranch = mapper.Map(dto,libraryBranch);
        mappedLibraryBranch.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        mappedLibraryBranch.UpdatedAt = DateTime.UtcNow;

        var result = await this.repository.UpdateAsync(mappedLibraryBranch);
        return this.mapper.Map<LibraryBranchForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var libraryBranch = await this.repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (libraryBranch == null)
            throw new TahseenException(404, "LibraryBranch not found");

        await fileUploadService.FileDeleteAsync(libraryBranch.Image);

        return await this.repository.DeleteAsync(libraryBranch.Id);
    }

    public async Task<IEnumerable<LibraryBranchForResultDto>> RetrieveAllAsync()
    {
        var result = this.repository.SelectAll().Where(l => !l.IsDeleted);
        foreach (var res in result)
        {
            res.Image = $"https://localhost:7020/{res.Image.Replace('\\', '/').TrimStart('/')}";
        }
        return this.mapper.Map<IEnumerable<LibraryBranchForResultDto>>(result);
    }

    public async Task<LibraryBranchForResultDto> RetrieveByIdAsync(long id)
    {
        var libraryBranch = await this.repository.SelectByIdAsync(id);
        if (libraryBranch == null || libraryBranch.IsDeleted)
            throw new TahseenException(404, "LibraryBranch not found");

        libraryBranch.Image = $"https://localhost:7020/{libraryBranch.Image.Replace('\\', '/').TrimStart('/')}";
        return mapper.Map<LibraryBranchForResultDto>(libraryBranch);
    }
}
