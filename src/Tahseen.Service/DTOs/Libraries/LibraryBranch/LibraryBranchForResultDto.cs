using Tahseen.Domain.Entities.Librarians;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Libraries.LibraryBranch;

public class LibraryBranchForResultDto
{
    public long Id { get; set; } 
    public string Name { get; set; }
    public string Address { get; set; }
    public string Image {  get; set; }
    public string PhoneNumber { get; set; }
    public string OpeningHours { get; set; }
    public LibraryType LibraryType { get; set; }
    public IEnumerable<Librarian> Librarians { get; set; }
}
