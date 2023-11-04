using System.Data;
using System;
using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities.Librarians;

public class Librarian:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string DateOfBirth { get; set; }
    public string Image { get; set; }
    public long LibraryBranchId { get; set; }
    public LibraryBranch LibraryBranch { get; set; }
    public Roles Roles { get; set; }

}