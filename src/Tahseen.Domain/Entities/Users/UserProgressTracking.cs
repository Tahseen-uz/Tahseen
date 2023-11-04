using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Domain.Entities;

public class UserProgressTracking:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long BookId { get; set; }
    public Book Books { get; set; }
    public long CurrentPage { get; set; }
    public long TotalPages { get; set; }
}