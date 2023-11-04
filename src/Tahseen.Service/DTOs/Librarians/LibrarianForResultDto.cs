using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Librarians;

public class LibrarianForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string DateOfBirth { get; set; }
    public string Image { get; set; }
    public long LibraryBranchId { get; set; }
    //public LibraryBranchForResultDto LibraryBranch { get; set; }
    public Roles Roles { get; set; }
}
