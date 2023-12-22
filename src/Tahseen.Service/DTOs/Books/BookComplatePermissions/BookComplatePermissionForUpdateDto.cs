using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities;

namespace Tahseen.Service.DTOs.Books.BookComplatePermissions;

public class BookComplatePermissionForUpdateDto
{
    public long UserId { get; set; }
    public long BookId { get; set; }
    public long LibraryBranchId { get; set; }
    public string Comment { get; set; }
}
