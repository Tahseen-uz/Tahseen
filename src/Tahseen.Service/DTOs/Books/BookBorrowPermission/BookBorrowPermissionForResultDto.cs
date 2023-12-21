using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Books.BookBorrowPermission
{
    public class BookBorrowPermissionForResultDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public UserForResultDto User { get; set; }
        public long BookId { get; set; }
        public BookForResultDto Book { get; set; }
        public long LibraryBranchId { get; set; }
        public LibraryBranchForResultDto LibraryBranch { get; set; }
    }
}
