using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Domain.Entities.Feedbacks;

public class Feedback : Auditable
{
    public long  UserId { get; set; }
    public User User { get; set; }

    public long  BookId { get; set; }
    public Book Book { get; set; }

    public string Comment { get; set; }
    public decimal Rating { get; set; }
    public DateTime Date { get; set; }
}