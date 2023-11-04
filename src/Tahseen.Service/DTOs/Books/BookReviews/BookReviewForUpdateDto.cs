namespace Tahseen.Service.DTOs.Books.BookReviews;
public class BookReviewForUpdateDto
{
    public long UserId { get; set; }
    public long BookId { get; set; }
    public decimal Rating { get; set; }
}
