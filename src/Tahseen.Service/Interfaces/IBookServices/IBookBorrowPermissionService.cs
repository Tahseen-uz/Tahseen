using Tahseen.Service.DTOs.Books.BookBorrowPermission;

namespace Tahseen.Service.Interfaces.IBookServices
{
    public interface IBookBorrowPermissionService
    {
        public Task<BookBorrowPermissionForResultDto> AddAsync(BookBorrowPermissionForCreationDto dto);
        public Task<bool> RemoveAsync(long id);
        public Task<BookBorrowPermissionForResultDto> RetrieveByIdAsync(long id);
        public Task<IEnumerable<BookBorrowPermissionForResultDto>> RetrieveAllBookByLibraryBranchIdAsync(long Id); //libraryBranchId
    }
}
