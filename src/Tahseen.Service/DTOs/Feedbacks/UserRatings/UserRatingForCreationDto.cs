namespace Tahseen.Service.DTOs.Feedbacks.UserRatings;

public class UserRatingForCreationDto
{
    public long UserId { get; set; }
    public long BooksCompleted { get; set; }
    public decimal Rating { get; set; }
}
