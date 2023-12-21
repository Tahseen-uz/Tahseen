using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IBookServices;
using Tahseen.Service.DTOs.Books.BookBorrowPermission;
using Microsoft.EntityFrameworkCore;

namespace Tahseen.Service.Services.Books
{
    public class BookBorrowPermissionService : IBookBorrowPermissionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BorrowedBook> _borrowedBookRepository;
        private readonly IRepository<LibraryBranch> _libraryBranchRepository;
        private readonly IRepository<BookBorrowPermission> _bookBorrowPermissionRepository;

        public BookBorrowPermissionService(
            IMapper mapper,
            IRepository<User> userRepository,
            IRepository<Book> bookRepository,
            IRepository<LibraryBranch> libraryBranchRepository,
            IRepository<BookBorrowPermission> bookBorrowPermissionRepository,
            IRepository<BorrowedBook> borrowedBookRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _libraryBranchRepository = libraryBranchRepository;
            _bookBorrowPermissionRepository = bookBorrowPermissionRepository;
            _borrowedBookRepository = borrowedBookRepository;
        }
        public async Task<BookBorrowPermissionForResultDto> AddAsync(BookBorrowPermissionForCreationDto dto)
        {
            var user = await _userRepository.SelectAll().
                Where(u => u.Id == dto.UserId && u.IsDeleted == false).
                AsNoTracking().
                FirstOrDefaultAsync();

            if (user is null)
                throw new TahseenException(404, "User is not found");

            var book = await _bookRepository.SelectAll().
                Where(b => b.Id == dto.BookId && b.IsDeleted == false).
                AsNoTracking().
                FirstOrDefaultAsync();

            if (book is null)
                throw new TahseenException(404, "Book is not found");

            else if (book.AvailableCopies < 1)
            {
                throw new TahseenException(404, "Book is not available");

            }
            var library = await _libraryBranchRepository.SelectAll().
                Where(l => l.Id == dto.LibraryBranchId && l.IsDeleted == false).
                AsNoTracking().
                FirstOrDefaultAsync();

            if (library is null)
                throw new TahseenException(404, "Library is not found");

            var bookBorrowPermission = await _bookBorrowPermissionRepository.SelectAll().
                Where(b => b.IsDeleted == false && b.UserId == dto.UserId && b.BookId == dto.BookId && b.LibraryBranchId == dto.LibraryBranchId).
                AsNoTracking().
                FirstOrDefaultAsync();

            if (bookBorrowPermission is not null)
                throw new TahseenException(409, "BookRentalPermission is already exist");
            var CheckIfBorrowed = await _borrowedBookRepository.SelectAll().Where(e => e.UserId == dto.UserId && e.BookId == dto.BookId && e.IsDeleted == false).AsNoTracking().FirstOrDefaultAsync();
            if (CheckIfBorrowed != null)
            {
                throw new TahseenException(400, "You have already borrowed this book");
            }

            var result = _mapper.Map<BookBorrowPermission>(dto);
            var check = await _bookBorrowPermissionRepository.CreateAsync(result);

            return _mapper.Map<BookBorrowPermissionForResultDto>(check);
        }

        public async Task<bool> RemoveAsync(long id)
        {
            var bookRentalPermission = await _bookBorrowPermissionRepository.SelectAll().
                Where(b => b.Id == id && b.IsDeleted == false).
                AsNoTracking().
                FirstOrDefaultAsync();

            if (bookRentalPermission is null)
                throw new TahseenException(404, "BookRentalPermission is not found");

            return await _bookBorrowPermissionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BookBorrowPermissionForResultDto>> RetrieveAllBookByLibraryBranchIdAsync(long Id) //LibraryBranchId
        {
            var result = await _bookBorrowPermissionRepository.SelectAll().
                Where(r => r.IsDeleted == false && r.LibraryBranchId == Id)
                .Include(u => u.User)
                .Include(b => b.Book)
                .Include(l => l.LibraryBranch)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookBorrowPermissionForResultDto>>(result);
        }

        public async Task<BookBorrowPermissionForResultDto> RetrieveByIdAsync(long id)
        {
            var bookRentalPermission = await _bookBorrowPermissionRepository.SelectAll().
                Where(b => b.Id == id && b.IsDeleted == false)
                .Include(u => u.User)
                .Include(b => b.Book)
                .Include(l => l.LibraryBranch)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (bookRentalPermission is null)
                throw new TahseenException(404, "BookRentalPermission is not found");

            return _mapper.Map<BookBorrowPermissionForResultDto>(bookRentalPermission);
        }
    }
}
