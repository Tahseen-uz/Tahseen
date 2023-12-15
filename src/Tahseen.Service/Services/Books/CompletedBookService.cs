using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Domain.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.DTOs.Books.CompletedBooks;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Domain.Entities.Library;

namespace Tahseen.Service.Services.Books;

public class CompletedBookService : ICompletedBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<CompletedBooks> _repository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<LibraryBranch> _libraryBranchRepository;
    private readonly IUserRatingService userRatingService;
    public CompletedBookService(
        IMapper mapper,
        IRepository<CompletedBooks> repository,
        IRepository<Book> bookRepository,
        IRepository<User> userRepository,
        IUserRatingService userRatingService,
        IRepository<LibraryBranch> libraryBranchRepository)
    {
        this._mapper = mapper;
        this._repository = repository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        this.userRatingService = userRatingService;
        _libraryBranchRepository = libraryBranchRepository;
    }

    public async Task<CompletedBookForResultDto> AddAsync(CompletedBookForCreationDto dto)
    {
        var user = await _userRepository.SelectAll().
            Where(u => u.Id == dto.UserId && u.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user == null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll()
            .Where(b => b.Id == dto.BookId && b.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (book == null)
            throw new TahseenException(404, "Book is not found");

        var libraryBranch = await this._libraryBranchRepository.SelectAll().Where(l => l.Id == book.LibraryId).AsNoTracking().FirstOrDefaultAsync();

        if(libraryBranch == null)
        {
            throw new TahseenException(404, "This library branch is not found");
        }

        var mapped = this._mapper.Map<CompletedBooks>(dto);
        mapped.BookTitle = book.Title;
        mapped.LibraryBranchName = libraryBranch.Name;
        mapped.BookImage = book.BookImage;
        var result = await this._repository.CreateAsync(mapped);


        var TotalCompletedBooks = await this._repository.SelectAll().Where(e => e.UserId == dto.UserId).CountAsync();
        var ratingUpdate = new UserRatingForUpdateDto();
        ratingUpdate.BooksCompleted = TotalCompletedBooks + 1;
        await this.userRatingService.ModifyAsync(dto.UserId, ratingUpdate);

        return this._mapper.Map<CompletedBookForResultDto>(result);
    }

    public async Task<CompletedBookForResultDto> ModifyAsync(long id, CompletedBookForUpdateDto dto)
    {
        var completedBook = await this._repository.SelectAll()
            .Where(a => a.Id == id && a.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (completedBook == null)
            throw new TahseenException(404, "CompletedBook not found");

        var user = await _userRepository.SelectAll().
            Where(u => u.Id == dto.UserId && u.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user == null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll()
            .Where(b => b.Id == dto.BookId && b.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (book == null)
            throw new TahseenException(404, "Book is not found");

        var mapped = this._mapper.Map(dto, completedBook);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = this._repository.UpdateAsync(mapped);
        return this._mapper.Map<CompletedBookForResultDto>(result);
    }

    public Task<bool> RemoveAsync(long id)
    => this._repository.DeleteAsync(id);

    public async Task<IEnumerable<CompletedBookForResultDto>> RetrieveAllAsync(long Id) // id == userId
    {
        var bookCompleted = await this._repository
            .SelectAll()
            .Where(b => b.UserId == Id && b.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();
        

        return this._mapper.Map<IEnumerable<CompletedBookForResultDto>>(bookCompleted);
    }

    public async Task<CompletedBookForResultDto> RetrieveByIdAsync(long id)
    {
        var book = await this._repository
            .SelectAll()
            .Where(c => c.UserId == id && c.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (book is null)
            throw new TahseenException(404, "CompletedBook does not found");

        return this._mapper.Map<CompletedBookForResultDto>(book);
    }
}
