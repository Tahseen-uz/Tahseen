using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Feedbacks.UserRatings;

public class UserRatingForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserForResultDto User { get; set; }
    public long BooksCompleted { get; set; }
    public decimal Rating { get; set; }
}
