using System.Text.Json.Serialization;
using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;
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

    [JsonIgnore]
    public IEnumerable<Librarian> Librarians { get; set; }

    [JsonIgnore]
    public IEnumerable<Book> TotalBooks { get; set; }

}