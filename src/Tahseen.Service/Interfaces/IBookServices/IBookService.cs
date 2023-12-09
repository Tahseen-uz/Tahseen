using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Books.BookReviews;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface IBookService
{
    public Task<BookForResultDto> AddAsync(BookForCreationDto dto);
    public Task<BookForResultDto> ModifyAsync(long id, BookForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<BookForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<BookForResultDto>> RetrieveAllParticularBooksAsync(long libraryBranchId, PaginationParams @params); //HttpContext Take Id
    public Task<IEnumerable<BookForResultDto>> RetrieveAllPublicLibraryBooksAsync(PaginationParams @params); //HttpContext Take Id

}