using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities;

namespace Tahseen.Service.DTOs.Books.BookReviews;

public class BookReviewForCreationDto
{
    public long UserId { get; set; }
    public long BookId { get; set; }
    public decimal Rating { get; set; }
}
