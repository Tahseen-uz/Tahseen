namespace Tahseen.Service.DTOs.Feedbacks.Feedback;

public class FeedbackForCreationDto
{
    public long  UserId { get; set; }
    public long  BookId { get; set; }
    public string Comment { get; set; }
}