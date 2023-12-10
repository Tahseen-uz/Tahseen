using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.Interfaces.IBookServices;
using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Exceptions;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Domain.Entities.Library;
using static System.Reflection.Metadata.BlobBuilder;
using Tahseen.Service.Configurations;
using Tahseen.Service.Extensions;
using Tahseen.Domain.Enums;
using Tahseen.Domain.Entities.EBooks;
using Tahseen.Domain.Entities.Languages;

namespace Tahseen.Service.Services.Books;

public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Book> _repository;
    private readonly IRepository<Genre> _genreRepository;
    private readonly IFileUploadService _fileUploadService;
    private readonly IRepository<Author> _authorRepository;
    private readonly IRepository<Language> _languageRepository;
    private readonly IRepository<Publisher> _publisherRepository;
    private readonly IRepository<LibraryBranch> _libraryRepository;


    public BookService(
        IMapper mapper,
        IRepository<Book> repository,
        IRepository<Genre> genreRepository,
        IFileUploadService fileUploadService, 
        IRepository<Author> authorRepository,
        IRepository<Language> languageRepository,
        IRepository<Publisher> publisherRepository,
        IRepository<LibraryBranch> libraryRepository)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._fileUploadService = fileUploadService;
        this._libraryRepository = libraryRepository;
        _genreRepository = genreRepository;
        _authorRepository = authorRepository;
        _languageRepository = languageRepository;
        _publisherRepository = publisherRepository;
    }

    public async Task<BookForResultDto> AddAsync(BookForCreationDto dto)
    {
        var book = await this._repository.SelectAll()
            .Where(b => b.Title.ToLower() == dto.Title.ToLower() && b.LibraryId == dto.LibraryId && b.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (book is not null)
            throw new TahseenException(400, "Book is already exist");

        var existingGenre = await this._genreRepository.SelectAll()
        .Where(g => g.Id == dto.GenreId && g.IsDeleted == false).AsNoTracking()
        .FirstOrDefaultAsync();

        if (existingGenre is null)
        {
            throw new TahseenException(400, "Genre does not exist");
        }
        
        var publisher = await _publisherRepository.SelectAll().
            Where(p => p.Id == dto.PublisherId && p.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (publisher is null)
            throw new TahseenException(404, "Publisher is not found");

        var author = await _authorRepository.SelectAll().
            Where(a => a.IsDeleted == false && a.Id == dto.AuthorId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (author is null)
            throw new TahseenException(404, "Author is not found");

        var library = await _libraryRepository.SelectAll()
            .Where(l => l.IsDeleted == false && l.Id == dto.LibraryId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (library is null)
            throw new TahseenException(404, "Library is not found");

        var language = await _languageRepository.SelectAll().
            Where(l => l.IsDeleted == false && l.Id == dto.LanguageId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (language is null)
            throw new TahseenException(404, "Language is not found");

        var mapped = this._mapper.Map<Book>(dto);
        if(dto.BookImage != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "BooksAssets",
                FormFile = dto.BookImage,
            };
            var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);
            mapped.BookImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        }

        var result = await _repository.CreateAsync(mapped);
        return _mapper.Map<BookForResultDto>(result);
    }


    /// <summary>
    /// Do logic for user Student Pupil
    /// </summary>
    /// <returns></returns>


    public async Task<IEnumerable<BookForResultDto>> RetrieveAllParticularBooksAsync(long id, PaginationParams @params)
    {
        var books = await this._repository.SelectAll()
            .Where(e => e.IsDeleted == false && e.LibraryId == id)
            .Include(l => l.Author) 
            .Include(l => l.LibraryBranch)
            .Include(l => l.Publisher)
            .Include(l => l.Genre)
            .Include(l => l.Language)
            //.Include(b => b.Borrowers)
            //.ThenInclude(u => u.User)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        if(books.Count == 0)
        {
            var PublicLibraries = await this._libraryRepository.SelectAll()
                        .Where(l => l.LibraryType == LibraryType.Public && l.IsDeleted == false)
                        .AsNoTracking()
                        .ToListAsync();
            if (PublicLibraries == null)
            {
                throw new TahseenException(404, "NotFound");
            }
            var publicLibraryBookIds = PublicLibraries.Select(l => l.Id);

            var publicBooks = await this._repository.SelectAll()
                .Where(e => publicLibraryBookIds.Contains(e.LibraryId) && e.IsDeleted == false)
                .Include(l => l.Author)
                .Include(l => l.LibraryBranch)
                .Include(l => l.Publisher)
                .Include(l => l.Language)
                .Include(l => l.Genre)
                //.Include(b => b.Borrowers)
                //.ThenInclude(u => u.User)
                .ToPagedList(@params)
                .AsNoTracking()
                .ToListAsync();
            if (publicBooks != null)
            {
                return this._mapper.Map<IEnumerable<BookForResultDto>>(publicBooks);
            }
        }
            var result = this._mapper.Map<IEnumerable<BookForResultDto>>(books);
            return result;
    }





    public async Task<BookForResultDto> ModifyAsync(long id, BookForUpdateDto dto)
    {
        var book = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (book is null)
            throw new Exception("Book not found");

        if (dto != null && dto.BookImage != null)
        {
            if (book.BookImage != null)
            {
                await _fileUploadService.FileDeleteAsync(book.BookImage);

            }
            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "BooksAssets",
                FormFile = dto.BookImage,
            };
            var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

            book.BookImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
            book.Title = !string.IsNullOrEmpty(dto.Title) ? dto.Title : book.Title;
            book.Content = !string.IsNullOrEmpty(dto.Content) ? dto.Content : book.Content;
            book.TotalCopies = dto.TotalCopies != null ? dto.TotalCopies : book.TotalCopies;
            book.ShelfLocation = !string.IsNullOrEmpty(dto.ShelfLocation) ? dto.ShelfLocation : book.ShelfLocation;
        }
        if (dto != null && dto.BookImage == null)
        {
            book.BookImage = book.BookImage;
            book.Title = !string.IsNullOrEmpty(dto.Title) ? dto.Title : book.Title;
            book.Content = !string.IsNullOrEmpty(dto.Content) ? dto.Content : book.Content;
            book.TotalCopies = dto.TotalCopies != null ? dto.TotalCopies : book.TotalCopies;
            book.ShelfLocation = !string.IsNullOrEmpty(dto.ShelfLocation) ? dto.ShelfLocation : book.ShelfLocation;
        }

        book.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(book);
        return _mapper.Map<BookForResultDto>(book);
        //Done
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var book = await this._repository.SelectAll()
            .Where(b => b.Id == id && !b.IsDeleted)
            .FirstOrDefaultAsync();
        if (book is null)
            throw new TahseenException(404, "Book is not found");
        if (book.BookImage != null)
            await _fileUploadService.FileDeleteAsync(book.BookImage);

        return await _repository.DeleteAsync(id);
    }

    public async Task<BookForResultDto> RetrieveByIdAsync(long id)
    {
        var book = await this._repository.SelectAll()
                       .Where(e => e.IsDeleted == false && e.Id == id)
                              .Include(l => l.Author)
                              .Include(l => l.Publisher)
                              .Include(l => l.LibraryBranch)
                              .Include(l => l.Genre)
                              .Include(l => l.Language)
                              //.Include(b => b.Borrowers.Where(a => a.IsDeleted == false))
                              //.ThenInclude(u => u.User)
                              .AsNoTracking()
                              .FirstOrDefaultAsync();
        if (book is not null)
        {
            return _mapper.Map<BookForResultDto>(book);
        }

        throw new TahseenException(404, "Book does not found");
    }

    public async Task<IEnumerable<BookForResultDto>> RetrieveAllPublicLibraryBooksAsync(PaginationParams @params)
    {
        var PublicLibraries = await this._libraryRepository.SelectAll()
            .Where(l => l.LibraryType == LibraryType.Public && l.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();
        if (PublicLibraries == null)
        {
            throw new TahseenException(404, "NotFound");
        }
        var publicLibraryBookIds = PublicLibraries.Select(l => l.Id);

        var publicBooks = await this._repository.SelectAll()
            .Where(e => publicLibraryBookIds.Contains(e.LibraryId) && e.IsDeleted == false)
            .Include(l => l.Author)
            .Include(l => l.LibraryBranch)
            .Include(l => l.Publisher)
            .Include(l => l.Language)
            .Include(l => l.Genre)
            //.Include(b => b.Borrowers.Where(a => a.IsDeleted == false))
            //.ThenInclude(u => u.User)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();
        if (publicBooks != null)
        {
            return this._mapper.Map<IEnumerable<BookForResultDto>>(publicBooks);
        }
        throw new TahseenException(404, "NotFound");

    }
}