using AutoMapper;
using Tahseen.Service.Exceptions;
using Tahseen.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Domain.Entities.AudioBooks;
using Tahseen.Service.DTOs.AudioBooks.AudioFile;
using Tahseen.Service.Interfaces.IAudioBookServices;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.AudiBooks;

public class AudioFileService : IAudioFileService
{
    private readonly IMapper _mapper;
    private readonly IRepository<AudioFile> _repository;
    private readonly IRepository<AudioBook> _audioBookRepository;
    private readonly IFileUploadService _fileUploadService;

    public AudioFileService(
        IMapper mapper,
        IRepository<AudioFile> repository,
        IRepository<AudioBook> audioBookRepository,
        IFileUploadService fileUploadService)
    {
        _mapper = mapper;
        _repository = repository;
        _audioBookRepository = audioBookRepository;
        _fileUploadService = fileUploadService;
    }
    public async Task<AudioFileForResultDto> AddAsync(AudioFileForCreationDto dto)
    {
         var audioBook = await _audioBookRepository.SelectAll()
            .Where(a => a.Id == dto.AudioBookId && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (audioBook is null)
            throw new TahseenException(404, "AudioBook is not found");

        
        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "AudioFiles",
            FormFile = dto.FilePath,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = _mapper.Map<AudioFile>(dto);
        mapped.FilePath = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);

        var result = await _repository.CreateAsync(mapped);

        return _mapper.Map<AudioFileForResultDto>(result);
    }

    public async Task<AudioFileForResultDto> ModifyAsync(long id, AudioFileForUpdateDto dto)
    {
        var audioBook = await _audioBookRepository.SelectAll()
            .Where(a => a.Id == dto.AudioBookId && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (audioBook is null)
            throw new TahseenException(404, "AudioBook is not found");

        var audioFile = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (audioFile is null)
            throw new TahseenException(404, "AudioFile is not found");

        await _fileUploadService.FileDeleteAsync(audioFile.FilePath);

        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "AudioFiles",
            FormFile = dto.FilePath,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = _mapper.Map(dto, audioFile);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.FilePath = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);

        var result = await _repository.UpdateAsync(mapped);

        return _mapper.Map<AudioFileForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var audioFile = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (audioFile is null)
            throw new TahseenException(404, "AudioFile is not found");

        await _fileUploadService.FileDeleteAsync(audioFile.FilePath);

        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AudioFileForResultDto>> RetrieveAllAsync()
    {
        var results = await this._repository.SelectAll()
                    .Where(a => !a.IsDeleted)
                    .AsNoTracking()
                    .ToListAsync();

    
        return _mapper.Map<IEnumerable<AudioFileForResultDto>>(results);
    }

    public async Task<AudioFileForResultDto> RetrieveByIdAsync(long id)
    {
        var audioFile = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (audioFile is null)
            throw new TahseenException(404, "AudioBook is not found");


        return _mapper.Map<AudioFileForResultDto>(audioFile);
    }
}
