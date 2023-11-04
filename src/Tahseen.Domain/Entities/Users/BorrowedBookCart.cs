using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Users
{
    public class BorrowedBookCart : Auditable
    {
        public long UserId { get; set; }
        public IEnumerable<BorrowedBook> BorrowedBook { get; set;}
    }
}
