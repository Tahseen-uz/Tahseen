using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Feedbacks.Feedback;

public class FeedbackForUpdateDto
{
    public long  UserId { get; set; }
    public UserForCreationDto User { get; set; }
    public long  BookId { get; set; }
    public string Comment { get; set; }
}