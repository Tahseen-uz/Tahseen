using AutoMapper;
using Tahseen.Domain.Entities;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.DTOs.Books.BookRentalPermission;
using Microsoft.EntityFrameworkCore;
using Tahseen.Service.Exceptions;

namespace Tahseen.Service.Services.Books;

public class BookRentalPermissionService : IBookRentalPermissionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<LibraryBranch> _libraryBranchRepository;
    private readonly IRepository<BookRentalPermission> _bookRentalPermissionRepository;

    public BookRentalPermissionService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Book> bookRepository,
        IRepository<LibraryBranch> libraryBranchRepository,
        IRepository<BookRentalPermission> bookRentalPermissionRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _libraryBranchRepository = libraryBranchRepository;
        _bookRentalPermissionRepository = bookRentalPermissionRepository;
    }
    public async Task<BookRentalPermissionForResultDto> AddAsync(BookRentalPermissionForCreationDto dto)
    {
        var user = await _userRepository.SelectAll().
            Where(u => u.Id == dto.UserId && u.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll().
            Where(b => b.Id == dto.BookId &&  b.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (book is null)
            throw new TahseenException(404, "Book is not found");

        var library = await _libraryBranchRepository.SelectAll().
            Where(l => l.Id == dto.LibraryBranchId && l.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (library is null)
            throw new TahseenException(404, "Library is not found");

        var bookRentalPermission = await _bookRentalPermissionRepository.SelectAll().
            Where(b => b.IsDeleted == false && b.UserId == dto.UserId && b.BookId == dto.BookId && b.LibraryBranchId == dto.LibraryBranchId).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (bookRentalPermission is not null)
            throw new TahseenException(409, "BookRentalPermission is already exist");

        var result = _mapper.Map<BookRentalPermission>(dto);
        var check = await _bookRentalPermissionRepository.CreateAsync(result);

        return _mapper.Map<BookRentalPermissionForResultDto>(check);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var bookRentalPermission = await _bookRentalPermissionRepository.SelectAll().
            Where(b => b.Id == id && b.IsDeleted == false).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (bookRentalPermission is null)
            throw new TahseenException(404, "BookRentalPermission is not found");

        return await _bookRentalPermissionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BookRentalPermissionForResultDto>> RetrieveAllAsync()
    {
        var result = await _bookRentalPermissionRepository.SelectAll().
            Where(r => r.IsDeleted == false).
            Include(u => u.User).
            Include(b => b.Book).
            Include(l => l.LibraryBranch).
            AsNoTracking().
            ToListAsync();

        return _mapper.Map<IEnumerable<BookRentalPermissionForResultDto>>(result);
    }

    public async Task<BookRentalPermissionForResultDto> RetrieveByIdAsync(long id)
    {
        var bookRentalPermission = await _bookRentalPermissionRepository.SelectAll().
            Where(b => b.Id == id && b.IsDeleted == false).
            Include(u => u.User).
            Include(b => b.Book).
            Include(l => l.LibraryBranch).
            AsNoTracking().
            FirstOrDefaultAsync();

        if (bookRentalPermission is null)
            throw new TahseenException(404, "BookRentalPermission is not found");

        return _mapper.Map<BookRentalPermissionForResultDto>(bookRentalPermission);
    }
}
