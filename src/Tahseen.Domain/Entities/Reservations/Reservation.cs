using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities.Reservations;

public class Reservation:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long BookId { get; set; }
    public Book Book { get; set; }

    public ReservationStatus ReservationStatus { get; set; }
}