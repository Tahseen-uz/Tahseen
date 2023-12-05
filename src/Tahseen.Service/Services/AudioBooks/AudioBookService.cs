using AutoMapper;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Domain.Entities.Narrators;
using Tahseen.Domain.Entities.AudioBooks;
using Tahseen.Service.DTOs.AudioBooks.AudioBook;
using Tahseen.Service.Interfaces.IAudioBookServices;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.AudiBooks;

public class AudioBookService : IAudioBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<AudioBook> _repository;
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<Genre> _genreRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IRepository<Narrator> _narratorRepository;

    public AudioBookService(
        IMapper mapper,
        IRepository<AudioBook> repository,
        IRepository<Author> authorRepository,
        IRepository<Genre> genreRepository,
        IFileUploadService fileUploadService,
        IRepository<Narrator> narratorRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _genreRepository = genreRepository;
        _authorRepository = authorRepository;
        _fileUploadService = fileUploadService;
        _narratorRepository = narratorRepository;
    }
    public async Task<AudioBookForResultDto> AddAsync(AudioBookForCreationDto dto)
    {
        var author = await _authorRepository.SelectAll()
            .Where(a => a.Id == dto.AuthorId && a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (author is null)
            throw new TahseenException(404, "Author is not found");

        var genre = await _genreRepository.SelectAll()
            .Where(g => g.Id == dto.GenreId && g.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (genre is null)
            throw new TahseenException(404, "Genre is not found");

        var narrator = await _narratorRepository.SelectAll()
           .Where(n => n.Id == dto.NarratorId && n.IsDeleted == false)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (narrator is null)
            throw new TahseenException(404, "Narrator is not found");

        var audioBook = await _repository.SelectAll()
            .Where(a => a.GenreId == dto.GenreId &&
            a.Title.ToLower() == dto.Title.ToLower() &&
            a.AuthorId == dto.AuthorId &&
            a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (audioBook is not null)
            throw new TahseenException(409, "Audio Book is already exist");

        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "AudioBooksAssets",
            FormFile = dto.Image,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = _mapper.Map<AudioBook>(dto);
        mapped.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);

        var result = await _repository.CreateAsync(mapped);

        return _mapper.Map<AudioBookForResultDto>(result);
    }

    public async Task<AudioBookForResultDto> ModifyAsync(long id, AudioBookForUpdateDto dto)
    {
        var author = await _authorRepository.SelectAll()
            .Where(a => a.Id == dto.AuthorId && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (author is null)
            throw new TahseenException(404, "Author is not found");

        var genre = await _genreRepository.SelectAll()
            .Where(g => g.Id == dto.GenreId && g.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (genre is null)
            throw new TahseenException(404, "Genre is not found");

        var audioBook = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();

        if (audioBook is null)
            throw new TahseenException(404, "AudioBook is not found");
        //Deleting Image
        await _fileUploadService.FileDeleteAsync(audioBook.Image);
        //Uploading Image
        var FileUploadForCreation = new FileUploadForCreationDto()
        {
            FolderPath = "AudioBookImages",
            FormFile = dto.Image,
        };
        var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = _mapper.Map(dto, audioBook);
        mapped.Image = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        mapped.UpdatedAt = DateTime.UtcNow;

        var result = await _repository.UpdateAsync(mapped);

        return _mapper.Map<AudioBookForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var audioBook = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (audioBook is null)
            throw new TahseenException(404, "AudioBook is not found");
        await _fileUploadService.FileDeleteAsync(audioBook.Image);

        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AudioBookForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var results = await this._repository.SelectAll()
            .Where(a => !a.IsDeleted)
            .Include(a => a.AudioFiles)
            .ToPagedList(@params)
            .Include(a => a.Author)
            .Include(g => g.Genre)
            .Include(n => n.Narrator)
            .AsNoTracking()
            .ToListAsync();

    
        return _mapper.Map<IEnumerable<AudioBookForResultDto>>(results);
    }

    public async Task<AudioBookForResultDto> RetrieveByIdAsync(long id)
    {
        var audioBook = await _repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .Include(a => a.Author)
            .Include(g => g.Genre)
            .Include(n => n.Narrator)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (audioBook is null)
            throw new TahseenException(404, "AudioBook is not found");


        return _mapper.Map<AudioBookForResultDto>(audioBook);
    }
}
