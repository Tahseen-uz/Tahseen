using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;

namespace Tahseen.Service.DTOs.Books.BookRentalPermission
{
    public class BookRentalPermissionForResultDto
    {
        public long UserId { get; set; }
        public UserForResultDto User { get; set; }
        public long BookId { get; set; }
        public BookForResultDto Book { get; set; }
        public long LibraryBranchId { get; set; }
        public LibraryBranchForResultDto LibraryBranch { get; set; }
    }
}
