using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;

namespace Tahseen.Service.DTOs.Books.BookComplatePermissions;

public class BookComplatePermissionForResultDto
{
    public long Id { get; set; }
    public UserForResultDto User { get; set; }
    public BookForResultDto Book { get; set; }
    public LibraryBranchForResultDto LibraryBranch { get; set; }
    public string Comment { get; set; }
}
