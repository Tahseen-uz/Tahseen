using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.BookComplatePermissions;

namespace Tahseen.Service.Interfaces.IBookServices;

public interface IBookComplatePermissionService
{
    public Task<BookComplatePermissionForResultDto> AddAsync(BookComplatePermissionForCreationDto dto);
    public Task<BookComplatePermissionForResultDto> ModifyAsync(long id, BookComplatePermissionForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<BookComplatePermissionForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<BookComplatePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
