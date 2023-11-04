using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Events;

namespace Tahseen.Domain.Entities.Library;

public class LibraryStatistics : Auditable
{
    public IEnumerable<User> TotalUsers { get; set;}
    public IEnumerable<Book> TotalBooks { get; set; }
    public IEnumerable<BorrowedBook> TotalBorrows { get; set; }
    public IEnumerable<Event> TotalEvents { get; set; }
}