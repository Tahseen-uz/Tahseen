using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Reservations;

public class ReservationForResultDto
{
    public long Id { get; set; }
    public long UserId {get; set;}
    public long BookId {get; set;}
    public ReservationStatus ReservationStatus { get; set; }
}