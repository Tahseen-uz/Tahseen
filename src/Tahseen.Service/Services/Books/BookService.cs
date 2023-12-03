using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.Interfaces.IFileUploadService;

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


    public async Task<IEnumerable<BookForResultDto>> RetrieveAllAsync(long? id, PaginationParams @params)
    {
        if (id > 0)
        {
            var library = await this._libraryRepository.SelectAll().Where(l => l.Id == id).FirstOrDefaultAsync();
            if (library == null || library.IsDeleted == true)
            {
                // Handle the case where the specified library branch does not exist or is deleted
                throw new TahseenException(404, "Library branch not found");
            }
            var books = await this._repository.SelectAll()
                .Where(e => e.IsDeleted == false && e.LibraryId == id)
                .Include(l => l.Author)
                .Include(l => l.LibraryBranch)
                .Include(l => l.Publisher)
                .Include(l => l.Genre)
                .ToPagedList(@params)
                .AsNoTracking()
                .ToListAsync();

            // Update BookImage URLs

            var result = this._mapper.Map<IEnumerable<BookForResultDto>>(books);
            foreach (var item in result)
            {
                item.BookFormat = item.BookFormat.ToString();
                item.Condition = item.Condition.ToString();
                item.Language = item.Language.ToString();
            }
            return result;
        }
        else if (id == null)
        {
            var allLibraries = this._libraryRepository.SelectAll().Where(e => e.IsDeleted == false && e.LibraryType == Domain.Enums.LibraryType.Public);
            var publicLibraryIds = allLibraries.Select(l => l.Id).ToList();
            var publicLibraryBooks = await this._repository.SelectAll()
                .Where(e => e.IsDeleted == false && publicLibraryIds.Contains(e.LibraryId))
                .Include(l => l.Author)
                .Include(l => l.LibraryBranch)
                .Include(l => l.Publisher)
                .Include(l => l.Genre)
                .ToPagedList(@params)
                .ToPagedList(@params)
                .AsNoTracking()
                .ToListAsync();



            return this._mapper.Map<IEnumerable<BookForResultDto>>(publicLibraryBooks);
        }
        else
        {
            // Handle invalid input for libraryBranchId (less than 0)
            throw new TahseenException(400, "Invalid library branch ID");
        }
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
            book.Language = dto.Language != null ? dto.Language : book.Language;
            book.TotalCopies = dto.TotalCopies != null ? dto.TotalCopies : book.TotalCopies;
            book.AvailableCopies = dto.AvailableCopies != null ? dto.AvailableCopies : book.AvailableCopies;
            book.Rating = dto.Rating != null ? dto.Rating : book.Rating;
            book.Reviews = dto.Reviews != null ? dto.Reviews : book.Reviews;
            book.BookFormat = dto.BookFormat != null ? dto.BookFormat : book.BookFormat;
            book.ShelfLocation = !string.IsNullOrEmpty(dto.ShelfLocation) ? dto.ShelfLocation : book.ShelfLocation;
            book.Condition = dto.Condition != null ? dto.Condition : book.Condition;
            book.AuthorId = dto.AuthorId != null ? dto.AuthorId : book.AuthorId;
            book.GenreId = dto.GenreId != null ? dto.GenreId : book.GenreId;
            book.PublisherId = dto.PublisherId != null ? dto.PublisherId : book.PublisherId;
            book.PrintedIn = !string.IsNullOrEmpty(dto.PrintedIn) ? dto.PrintedIn : book.PrintedIn;
        }
        if (dto != null && dto.BookImage == null)
        {
            book.BookImage = book.BookImage;
            book.Title = !string.IsNullOrEmpty(dto.Title) ? dto.Title : book.Title;
            book.Content = !string.IsNullOrEmpty(dto.Content) ? dto.Content : book.Content;
            book.Language = dto.Language != null ? dto.Language : book.Language;
            book.TotalCopies = dto.TotalCopies != null ? dto.TotalCopies : book.TotalCopies;
            book.AvailableCopies = dto.AvailableCopies != null ? dto.AvailableCopies : book.AvailableCopies;
            book.Rating = dto.Rating != null ? dto.Rating : book.Rating;
            book.Reviews = dto.Reviews != null ? dto.Reviews : book.Reviews;
            book.BookFormat = dto.BookFormat != null ? dto.BookFormat : book.BookFormat;
            book.ShelfLocation = !string.IsNullOrEmpty(dto.ShelfLocation) ? dto.ShelfLocation : book.ShelfLocation;
            book.Condition = dto.Condition != null ? dto.Condition : book.Condition;
            book.AuthorId = dto.AuthorId != null ? dto.AuthorId : book.AuthorId;
            book.GenreId = dto.GenreId != null ? dto.GenreId : book.GenreId;
            book.PrintedIn = !string.IsNullOrEmpty(dto.PrintedIn) ? dto.PrintedIn : book.PrintedIn;
            book.PublisherId = dto.PublisherId != null ? dto.PublisherId : book.PublisherId;
        }

        book.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(book);
        return _mapper.Map<BookForResultDto>(book);

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
                       .FirstOrDefaultAsync();
        if (book is not null)
        {
            return _mapper.Map<BookForResultDto>(book);
        }

        throw new TahseenException(404, "Book does not found");
    }
}