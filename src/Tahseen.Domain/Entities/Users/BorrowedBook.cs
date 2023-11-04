using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.Users;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities
{
    public class BorrowedBook : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public long BorrowedBookCartId { get; set; }
        public BorrowedBookCart BorrowedBookCart { get; set; }
        public long LibraryBranchId { get; set; }
        public LibraryBranch LibraryBranch { get; set; }
        public DateTime ReturnDate { get; set; }
        public BorrowedBookStatus Status { get; set; }
        public decimal FineAmount { get; set; }
    }
}
