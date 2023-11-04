
using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Feedbacks.Feedback;

public class FeedbackForResultDto
{
    public long Id { get; set; }
    public long  UserId { get; set; }
    public UserForCreationDto User { get; set; }
    public long  BookId { get; set; }
    public Book Book { get; set; }
    public string Comment { get; set; }
}