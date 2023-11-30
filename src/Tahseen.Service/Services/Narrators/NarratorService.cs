using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.AudioBooks;
using Tahseen.Domain.Entities.Narrators;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Narrators;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.INurratorServices;

namespace Tahseen.Service.Services.Narrators;

public class NarratorService : INarratorService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Narrator> _repository;
    private readonly IRepository<AudioBook> _audioBookRepository;
    private readonly IFileUploadService _fileUploadService;

    public NarratorService(
        IMapper mapper,
        IRepository<Narrator> repository, 
        IRepository<AudioBook> audioBookRepository,
        IFileUploadService fileUploadService)
    {
        _mapper = mapper;
        _repository = repository;
        _audioBookRepository = audioBookRepository;
        _fileUploadService = fileUploadService;
    }
    public async Task<NarratorForResultDto> AddAsync(NarratorForCreationDto dto)
    {
        var narrator = await _repository.SelectAll()
            .Where(n => n.FirstName.ToLower() == dto.FirstName.ToLower() &&
            n.LastName.ToLower() == dto.LastName.ToLower() &&
            n.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (narrator is not null)
            throw new TahseenException(409, "Narrator is already exist");

        var mapped = _mapper.Map<Narrator>(dto);
        if(dto.Image != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "NarratorsAssets",
                FormFile = dto.Image,
            };
            var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

            mapped.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        }    


        var result = await _repository.CreateAsync(mapped);

        return _mapper.Map<NarratorForResultDto>(result);
    }

    public async Task<NarratorForResultDto> ModifyAsync(long id, NarratorForUpdateDto dto)
    {
        var narrator = await _repository.SelectAll()
            .Where(n => n.Id ==  id && n.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (narrator is null)
            throw new TahseenException(404, "Narrator is not found");


        if(dto != null && dto.Image != null)
        {
            if(dto.Image != null)
            {
                await _fileUploadService.FileDeleteAsync(narrator.Image);
            }

            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "NarratorsAssets",
                FormFile = dto.Image,
            };
            var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

            narrator.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            narrator.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : narrator.FirstName;
            narrator.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : narrator.LastName;
        }

        if(dto != null && dto.Image == null)
        {
            narrator.Image = narrator.Image;
            narrator.FirstName = !string.IsNullOrEmpty(dto.FirstName) ? dto.FirstName : narrator.FirstName;
            narrator.LastName = !string.IsNullOrEmpty(dto.LastName) ? dto.LastName : narrator.LastName;
        }

        narrator.UpdatedAt = DateTime.UtcNow;

        var result = await _repository.UpdateAsync(narrator);

        return _mapper.Map<NarratorForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var narrator = await _repository.SelectAll()
            .Where(n => n.Id == id && n.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (narrator is null)
            throw new TahseenException(404, "Narrator is not found");

        if(narrator.Image != null)
        {
            await _fileUploadService.FileDeleteAsync(narrator.Image);
        }

        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<NarratorForResultDto>> RetrieveAllAsync()
    {
        var narrators = await _repository.SelectAll()
            .Where(n => n.IsDeleted == false)
            .Include(n => n.AudioBooks) // Include the AudioBooks navigation property
            .ThenInclude(ab => ab.AudioFiles) // If needed, include additional navigation properties within AudioBooks
            .AsNoTracking()
            .ToListAsync();

     

        return _mapper.Map<IEnumerable<NarratorForResultDto>>(narrators);
    }

    public async Task<NarratorForResultDto> RetrieveByIdAsync(long id)
    {
        var narrator = await _repository.SelectAll()
            .Where(n => n.Id == id && n.IsDeleted == false)
            .Include(n => n.AudioBooks) // Include the AudioBooks navigation property
            .ThenInclude(ab => ab.AudioFiles) // If needed, include additional navigation properties within AudioBooks
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (narrator is null)
            throw new TahseenException(404, "Narrator is not found");


        return _mapper.Map<NarratorForResultDto>(narrator);
    }
}
