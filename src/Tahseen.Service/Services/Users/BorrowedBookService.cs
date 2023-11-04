using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Users.BorrowedBook;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private readonly IRepository<BorrowedBook> BorrowedBook;
        private readonly IMapper _mapper;
        private readonly IBorrowBookCartService _bookCartService;
        public BorrowedBookService(IRepository<BorrowedBook> BorrowedBook, IMapper mapper, IBorrowBookCartService _bookCartService)
        {
            this._mapper = mapper;
            this.BorrowedBook = BorrowedBook;
            this._bookCartService = _bookCartService;
        }
        public async Task<BorrowedBookForResultDto> AddAsync(BorrowedBookForCreationDto dto)
        {
            //var Check = this.BorrowedBook.SelectAll().Where(b => b.UserId == dto.UserId && b.UserId == dto.UserId && b.BookId == dto.BookId && b.IsDeleted == false);
            var UserBorrowedBookCart = (await this._bookCartService.RetrieveAllAsync()).Where(e => e.UserId == dto.UserId).FirstOrDefault();
            var data = this._mapper.Map<BorrowedBook>(dto);
            data.BorrowedBookCartId = UserBorrowedBookCart.BorrowedBookCartId;
            var result = await this.BorrowedBook.CreateAsync(data);
            return this._mapper.Map<BorrowedBookForResultDto>(result);
        }

        public async Task<BorrowedBookForResultDto> ModifyAsync(long Id, BorrowedBookForUpdateDto dto)
        {
            var data = await this.BorrowedBook.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
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
            var result = this.BorrowedBook.SelectAll().Where(t => t.IsDeleted == false);
            return this._mapper.Map<IEnumerable<BorrowedBookForResultDto>>(result);
        }

        public async Task<BorrowedBookForResultDto> RetrieveByIdAsync(long Id)
        {
            var data = await this.BorrowedBook.SelectByIdAsync(Id);
            if(data != null && data.IsDeleted == false)
            {
                return this._mapper.Map<BorrowedBookForResultDto>(data);
            }
            throw new TahseenException(404, "Borrowed book is not found");
        }
    }
}
