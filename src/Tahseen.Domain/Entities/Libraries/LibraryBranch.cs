using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Librarians;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities.Library;
public class LibraryBranch:Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }
    public string PhoneNumber { get; set; }
    public string OpeningHours { get; set; }
    public LibraryType LibraryType { get; set; }
    public IEnumerable<Librarian> Librarians { get; set; } 
    public List<Pupil> Pupils { get; set; }
}