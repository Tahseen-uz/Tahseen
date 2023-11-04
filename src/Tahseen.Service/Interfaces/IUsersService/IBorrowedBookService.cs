using Tahseen.Service.DTOs.Users.BorrowedBook;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IBorrowedBookService
    {
        public Task<BorrowedBookForResultDto> AddAsync(BorrowedBookForCreationDto dto);
        public Task<BorrowedBookForResultDto> ModifyAsync(long Id, BorrowedBookForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<BorrowedBookForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<BorrowedBookForResultDto>> RetrieveAllAsync();
    }
}
