using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.BookComplatePermissions;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Extensions;
using Tahseen.Service.Interfaces.IBookServices;

namespace Tahseen.Service.Services.Books;

public class BookComplatePermissionService : IBookComplatePermissionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Book> _bookRepository;
    private readonly IRepository<LibraryBranch> _libraryBranchRepository;
    private readonly IRepository<BookCompletePermission> _bookCompletePermissionRepository;

    public BookComplatePermissionService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Book> bookRepository,
        IRepository<LibraryBranch> libraryBranchRepository,
        IRepository<BookCompletePermission> bookComplatePermissionRepository
        )
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _libraryBranchRepository = libraryBranchRepository;
        _bookCompletePermissionRepository = bookComplatePermissionRepository;
    }
    public async Task<BookComplatePermissionForResultDto> AddAsync(BookComplatePermissionForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId && u.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll()
            .Where(b => b.Id == dto.BookId && b.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (book is null)
            throw new TahseenException(404, "Book is not found");

        var library = await _libraryBranchRepository.SelectAll()
            .Where(l => l.Id == dto.LibraryBranchId && l.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (library is null)
            throw new TahseenException(404, "Library Branch is not found");

        var bookComplatePermission = await _bookCompletePermissionRepository.SelectAll()
            .Where(bcp => bcp.UserId == dto.UserId && bcp.BookId == dto.BookId && bcp.LibraryBranchId == dto.LibraryBranchId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (bookComplatePermission is not null)
            throw new TahseenException(409, "BookComplatePermission is already exist");

        var mapped = _mapper.Map<BookCompletePermission>(dto);
        var result = await _bookCompletePermissionRepository.CreateAsync(mapped);

        return _mapper.Map<BookComplatePermissionForResultDto>(result);
    }

    public async Task<BookComplatePermissionForResultDto> ModifyAsync(long id, BookComplatePermissionForUpdateDto dto)
    {
        var bookComplatePermission = await _bookCompletePermissionRepository.SelectAll()
            .Where(bcp => bcp.Id == id && bcp.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (bookComplatePermission is null)
            throw new TahseenException(404, "BookComplatePermission is not found");

        var user = await _userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId && u.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var book = await _bookRepository.SelectAll()
            .Where(b => b.Id == dto.BookId && b.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (book is null)
            throw new TahseenException(404, "Book is not found");

        var library = await _libraryBranchRepository.SelectAll()
            .Where(l => l.Id == dto.LibraryBranchId && l.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (library is null)
            throw new TahseenException(404, "Library Branch is not found");

        var mapped = _mapper.Map(dto,bookComplatePermission);
        mapped.UpdatedAt = DateTime.UtcNow;

        var result = await _bookCompletePermissionRepository.UpdateAsync(mapped);

        return _mapper.Map<BookComplatePermissionForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var bookComplatePermission = await _bookCompletePermissionRepository.SelectAll()
            .Where(bcp => bcp.Id == id && bcp.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (bookComplatePermission is null)
            throw new TahseenException(404, "BookComplatePermission is not found");

        return await _bookCompletePermissionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BookComplatePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var bookComplatePermissons = await _bookCompletePermissionRepository.SelectAll()
            .Where(bcp => bcp.IsDeleted == false)
            .Include(u => u.User)
            .Include(b => b.Book)
            .Include(l => l.LibraryBranch)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<IEnumerable<BookComplatePermissionForResultDto>>(bookComplatePermissons);
    }

    public async Task<BookComplatePermissionForResultDto> RetrieveByIdAsync(long id)
    {
        var bookComplatePermission = await _bookCompletePermissionRepository.SelectAll()
            .Where(bcp => bcp.Id == id && bcp.IsDeleted == false)
            .Include(u => u.User)
            .Include(b => b.Book)
            .Include(l => l.LibraryBranch)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (bookComplatePermission is null)
            throw new TahseenException(404, "BookComplatePermission is not found");

        return _mapper.Map<BookComplatePermissionForResultDto>(bookComplatePermission);
    }
}
