using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities;

public class Fine:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long BookId { get ; set; }   
    public Book Book { get; set; }
    public decimal Amount { get; set; }
    public string Reason { get; set; }
    public FineStatus Status { get; set; }
}