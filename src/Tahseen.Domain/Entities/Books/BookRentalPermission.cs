using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Library;

namespace Tahseen.Domain.Entities.Books
{
    public class BookRentalPermission : Auditable
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long BookId { get; set; }
        public Book Book { get; set; }
        public long LibraryBranchId { get; set; }
        public LibraryBranch LibraryBranch { get; set; }

    }
}
