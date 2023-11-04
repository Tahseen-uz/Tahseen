using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Books.BookReviews;

public class BookReviewForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserForResultDto User { get; set; }
    public long BookId { get; set; }
    //public BookForResultDto Book { get; set; }
    public decimal Rating { get; set; }
}
