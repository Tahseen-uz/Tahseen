using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Books;

public class CompletedBooks:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long BookId { get; set; }
    public Book Book { get; set; }  
    public string Comment { get; set; }
}