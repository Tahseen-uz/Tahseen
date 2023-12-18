using Tahseen.Service.DTOs.Books.BookRentalPermission;
using Tahseen.Service.DTOs.Books.BookReviews;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface IBookRentalPermissionService
{
    public Task<BookRentalPermissionForResultDto> AddAsync(BookRentalPermissionForCreationDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<BookRentalPermissionForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<BookRentalPermissionForResultDto>> RetrieveAllAsync();
}
