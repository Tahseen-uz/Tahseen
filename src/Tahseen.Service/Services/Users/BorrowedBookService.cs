using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Users.BorrowedBook;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly IRepository<BorrowedBook> BorrowedBook;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly IBorrowBookCartService _bookCartService;
        public BorrowedBookService(
            IRepository<BorrowedBook> BorrowedBook, 
            IMapper mapper, 
            IBorrowBookCartService _bookCartService, 
            IRepository<User> userRepository, 
            IRepository<Book> bookRepository)
        {
            this._mapper = mapper;
            this.BorrowedBook = BorrowedBook;
            this._bookCartService = _bookCartService;
            this._userRepository = userRepository;
            this._bookRepository = bookRepository;
        }
        public async Task<BorrowedBookForResultDto> AddAsync(BorrowedBookForCreationDto dto)
        {
            //var Check = this.BorrowedBook.SelectAll().Where(b => b.UserId == dto.UserId && b.UserId == dto.UserId && b.BookId == dto.BookId && b.IsDeleted == false);
            var UserBorrowedBookCart = (await this._bookCartService.RetrieveAllAsync())
                            .Where(e => e.UserId == dto.UserId)
                            .FirstOrDefault();
            var userLibraryBranch = await this._userRepository.SelectAll().Where(e => e.Id == dto.UserId && e.IsDeleted == false).FirstOrDefaultAsync();
            var BorrowedBooks = await this.BorrowedBook.SelectAll().Where(e => e.UserId == dto.UserId && e.IsDeleted == false).FirstOrDefaultAsync();
            var SelectedBook = await this._bookRepository.SelectAll().Where(b => b.Id == dto.BookId && b.IsDeleted == false).FirstOrDefaultAsync();
            if (BorrowedBooks != null && BorrowedBooks.BookId == dto.BookId)
            {
                throw new TahseenException(409, "You have already this book in your cart");
            }
            var data = this._mapper.Map<BorrowedBook>(dto);
            if(userLibraryBranch != null)
            {
                data.LibraryBranchId = userLibraryBranch.LibraryBranchId;
            }
            data.BorrowedBookCartId = UserBorrowedBookCart.Id;
            data.ReturnDate = DateTime.UtcNow.AddDays(10); // Assuming you want to add 10 days
            data.BookTitle = SelectedBook.Title;
            data.Status = 0;
            data.FineAmount = 0;
            if(SelectedBook != null && SelectedBook.AvailableCopies > 0)
            {
                SelectedBook.AvailableCopies = SelectedBook.AvailableCopies - 1;
                await _bookRepository.UpdateAsync(SelectedBook);
            }
            else
            {
                throw new TahseenException(400, "Book is not available for now");
            }
            var result = await this.BorrowedBook.CreateAsync(data);
            return this._mapper.Map<BorrowedBookForResultDto>(result);
        }

        public async Task<BorrowedBookForResultDto> ModifyAsync(long Id, BorrowedBookForUpdateDto dto)
        {
            var data = await this.BorrowedBook.SelectAll()
                .Where(e => e.Id == Id && e.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data is not null)
            {
                var MappedData = this._mapper.Map(dto, data);
                MappedData.UpdatedAt = DateTime.UtcNow;
                var result = await BorrowedBook.UpdateAsync(MappedData);
                return this._mapper.Map<BorrowedBookForResultDto>(result);
            }
            throw new TahseenException(404, "Borrowed book is not found");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this.BorrowedBook.DeleteAsync(Id);
        }

        public async Task<IEnumerable<BorrowedBookForResultDto>> RetrieveAllAsync()
        {
            var result = await this.BorrowedBook
                .SelectAll()
                .Where(t => t.IsDeleted == false)
                .Include(b => b.Book)
                .Include(u => u.User)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            var res = this._mapper.Map<IEnumerable<BorrowedBookForResultDto>>(result);
            foreach (var item in res)
            {
                item.Status = item.Status.ToString();
                item.BookTitle = item.BookTitle.ToString();
            }
            return res;
        }

        public async Task<BorrowedBookForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await this.BorrowedBook.SelectAll()
                .Where(t => t.IsDeleted == false)
                .Include(b => b.Book)
                .Include(u => u.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(); 
            if (data != null && data.IsDeleted == false)
            {
                var result = this._mapper.Map<BorrowedBookForResultDto>(data);
                result.Status = result.Status.ToString();
                result.BookTitle = result.BookTitle.ToString();
                return result;
            }
            throw new TahseenException(404, "Borrowed book is not found");
        }
    }
}
