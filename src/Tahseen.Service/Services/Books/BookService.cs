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

namespace Tahseen.Service.Services.Books;

public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Book> _repository;
    private readonly IRepository<LibraryBranch> _libraryRepository;
    private readonly IFileUploadService _fileUploadService;


    public BookService(IMapper mapper, IRepository<Book> repository, IFileUploadService fileUploadService, IRepository<LibraryBranch> libraryRepository)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._fileUploadService = fileUploadService;
        this._libraryRepository = libraryRepository;

    }

    public async Task<BookForResultDto> AddAsync(BookForCreationDto dto)
    {
        var book = await this._repository.SelectAll()
            .Where(b => b.Title.ToLower() == dto.Title.ToLower() && b.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (book is not null)
            throw new TahseenException(400, "Book is already exist");

        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "BooksAssets",
            FormFile = dto.BookImage,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = this._mapper.Map<Book>(dto);
        mapped.BookImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        var result = await _repository.CreateAsync(mapped);
        return _mapper.Map<BookForResultDto>(result);
    }


    /// <summary>
    /// Do logic for user Student Pupil
    /// </summary>
    /// <returns></returns>


    public async Task<IEnumerable<BookForResultDto>> RetrieveAllAsync(long id, PaginationParams @params)
    {
            var books = await this._repository.SelectAll()
                .Where(e => e.IsDeleted == false && e.LibraryId == id)
                .Include(l => l.Author)
                .Include(l => l.LibraryBranch)
                .Include(l => l.Publisher)
                .Include(l => l.Genre)
                .ToPagedList(@params)
                .AsNoTracking()
                .ToListAsync();

            if(books != null)
            {
                var result = this._mapper.Map<IEnumerable<BookForResultDto>>(books);
            foreach (var item in result)
            {
                item.BookFormat = item.BookFormat.ToString();
                item.Condition = item.Condition.ToString();
                item.Language = item.Language.ToString();
            }
                return result;
            }
            throw new TahseenException(404, "NotFound");
    }





    public async Task<BookForResultDto> ModifyAsync(long id, BookForUpdateDto dto)
    {
        var book = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (book is null)
            throw new Exception("Book not found");

        // delete image
        await _fileUploadService.FileDeleteAsync(book.BookImage);

        // uploading image
        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "BooksAssets",
            FormFile = dto.BookImage,
        };
        var FileResult = await this._fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mappedBook = _mapper.Map(dto, book);
        mappedBook.UpdatedAt = DateTime.UtcNow;
        mappedBook.BookImage = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        await _repository.UpdateAsync(mappedBook);
        return _mapper.Map<BookForResultDto>(mappedBook);

    }

    public async Task<bool> RemoveAsync(long id)
    {
        var book = await this._repository.SelectAll()
            .Where(b => b.Id == id && !b.IsDeleted)
            .FirstOrDefaultAsync();
        if (book is null)
            throw new TahseenException(404, "Book is not found");

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
        if(PublicLibraries == null){
            throw new TahseenException(404, "NotFound");
        }
        var publicLibraryBookIds = PublicLibraries.Select(l => l.Id);

        var publicBooks = await this._repository.SelectAll()
            .Where(e => publicLibraryBookIds.Contains(e.LibraryId))
            .Include(l => l.Author)
            .Include(l => l.LibraryBranch)
            .Include(l => l.Publisher)
            .Include(l => l.Genre)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();
        if( publicBooks != null )
        {
            return this._mapper.Map<IEnumerable<BookForResultDto>>(publicBooks);
        }
        throw new TahseenException(404, "NotFound");

    }
}