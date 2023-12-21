using System.ComponentModel.DataAnnotations.Schema;
using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Library;

namespace Tahseen.Domain.Entities.Books;

public class CompletedBooks : Auditable
{
    public long UserId { get; set; }
    public long BookId { get; set; }
    public long? LibraryBranchId { get; set; }
    public string Comment { get; set; }
    public User User { get; set; }
    public Book Book { get; set; }
    public virtual LibraryBranch LibraryBranch { get; set; }
}