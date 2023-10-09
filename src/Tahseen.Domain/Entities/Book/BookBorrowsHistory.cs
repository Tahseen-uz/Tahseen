using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Book;

public class BookBorrowsHistory:Auditable
{
    public long BookId { get; set; }
    public long UserId { get; set; }
}
 