using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.EBooks;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.EBooks.EBookFile;
using Tahseen.Service.Interfaces.IEBookServices;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.EBooks;

public class EBookFileService : IEBookFileService
{
    private readonly IMapper _mapper;
    private readonly IRepository<EBookFile> _repository;
    private readonly IRepository<EBook> _eBookRepository;
    private readonly IFileUploadService _fileUploadService;

    public EBookFileService(
        IMapper mapper,
        IRepository<EBookFile> repository,
        IRepository<EBook> eBookRepository,
        IFileUploadService fileUploadService)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._eBookRepository = eBookRepository;
        this._fileUploadService = fileUploadService;
    }
    public async Task<EBookFileForResultDto> AddAsync(EBookFileForCreationDto dto)
    {
        var eBook = await _eBookRepository.SelectAll()
            .Where(e => e.Id == dto.EBookId && e.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (eBook is null)
            throw new TahseenException(404, "EBook is not found");

        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "EBookFiles",
            FormFile = dto.FilePath,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = this._mapper.Map<EBookFile>(dto);
        mapped.FilePath = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);

        var result = await this._repository.CreateAsync(mapped);

        return this._mapper.Map<EBookFileForResultDto>(result);
    }

    public async Task<EBookFileForResultDto> ModifyAsync(long id, EBookFileForUpdateDto dto)
    {
        var eBook = await this._eBookRepository.SelectAll()
            .Where(e => e.Id == dto.EBookId && e.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (eBook is null)
            throw new TahseenException(404, "EBook is not found");

        var eBookFile = await this._repository.SelectAll()
            .Where(e => e.Id == id && e.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (eBookFile is null)
            throw new TahseenException(404, "EBookFile is not found");

        await this._fileUploadService.FileDeleteAsync(eBookFile.FilePath);

        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "EBookFiles",
            FormFile = dto.FilePath,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = this._mapper.Map(dto, eBookFile);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.FilePath = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);

        var result = await this._repository.UpdateAsync(mapped);

        return this._mapper.Map<EBookFileForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var eBookFile = await this._repository.SelectAll()
            .Where(e => e.Id == id && e.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (eBookFile is null)
            throw new TahseenException(404, "EBookFile is not found");

        await this._fileUploadService.FileDeleteAsync(eBookFile.FilePath);

        return await this._repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EBookFileForResultDto>> RetrieveAllAsync()
    {
        var results = await this._repository.SelectAll()
                    .Where(e => !e.IsDeleted)
                    .Include(e => e.EBook)
                    .AsNoTracking()
                    .ToListAsync();

        return this._mapper.Map<IEnumerable<EBookFileForResultDto>>(results);
    }

    public async Task<EBookFileForResultDto> RetrieveByIdAsync(long id)
    {
        var eBookFile = await this._repository.SelectAll()
            .Where(e => e.Id == id && e.IsDeleted == false)
            .Include(e => e.EBook)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (eBookFile is null)
            throw new TahseenException(404, "EBookFile is not found");

        return this._mapper.Map<EBookFileForResultDto>(eBookFile);
    }
}
