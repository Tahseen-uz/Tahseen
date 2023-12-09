using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Libraries.LibraryBranch;

namespace Tahseen.Service.DTOs.Librarians;

public class LibrarianForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string DateOfBirth { get; set; }
    public string Image { get; set; }
    public long LibraryBranchId { get; set; }
    public LibraryBranchForResultDto LibraryBranch { get; set; }
    public string Roles { get; set; }

    //public LibraryBranchForResultDto LibraryBranch { get; set; }
}
