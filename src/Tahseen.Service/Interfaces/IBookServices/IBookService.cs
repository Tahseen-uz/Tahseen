using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Books.BookReviews;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface IBookService
{
    public Task<BookForResultDto> AddAsync(BookForCreationDto dto);
    public Task<BookForResultDto> ModifyAsync(long id, BookForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<BookForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<BookForResultDto>> RetrieveAllAsync(long? libraryBranchId); //HttpContext Take Id

}