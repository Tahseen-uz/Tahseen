using System.Text.Json.Serialization;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Librarians;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Books.Book;
using Tahseen.Service.DTOs.Librarians;

namespace Tahseen.Service.DTOs.Libraries.LibraryBranch;

public class LibraryBranchForResultDto
{
    public long Id { get; set; } 
    public string Name { get; set; }
    public string Address { get; set; }
    public string Image {  get; set; }
    public string PhoneNumber { get; set; }
    public string OpeningHours { get; set; }
    public string LibraryType { get; set; }


    [JsonIgnore]
    public IEnumerable<LibrarianForResultDto> Librarians { get; set; }
    [JsonIgnore]
    public IEnumerable<BookForResultDto> TotalBooks { get; set; }

}
