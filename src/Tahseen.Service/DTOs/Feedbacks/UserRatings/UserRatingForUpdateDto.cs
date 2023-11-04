using Tahseen.Domain.Entities;

namespace Tahseen.Service.DTOs.Feedbacks.UserRatings;

public class UserRatingForUpdateDto
{
    public long UserId { get; set; }
    public long BooksCompleted { get; set; }
    public decimal Rating { get; set; }
}