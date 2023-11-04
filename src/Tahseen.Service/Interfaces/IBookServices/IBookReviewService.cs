using Tahseen.Service.DTOs.Books.BookReviews;

namespace Tahseen.Service.Interfaces.IBookServices;
public interface IBookReviewService
{
    public Task<BookReviewForResultDto> AddAsync(BookReviewForCreationDto dto);
    public Task<BookReviewForResultDto> ModifyAsync(long id, BookReviewForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<BookReviewForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<BookReviewForResultDto>> RetrieveAllAsync();
}
