using Tahseen.Domain.Enums;
using Tahseen.Domain.Commons;
using System.Text.Json.Serialization;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Domain.Entities.Reservations;
using System.ComponentModel.DataAnnotations.Schema;
using Tahseen.Domain.Entities.Languages;

namespace Tahseen.Domain.Entities.Books;

public class Book : Auditable
{
    public string Title { get; set; }
    public long TotalCopies { get; set; }
    public long AvailableCopies { get; set; }
    public string Content { get; set; }
    public string ShelfLocation { get; set; }
    public string BookImage { get; set;}
    public long TotalPages { get; set; }
    
    public long AuthorId { get; set; }
    [ForeignKey("AuthorId")]
    public Author Author { get; set; }
    public long GenreId { get; set; }
    [ForeignKey("GenreId")]
    public Genre Genre { get; set; }
    
    public long LibraryId { get; set; }
    [ForeignKey("LibraryId")]
    [JsonIgnore]
    public LibraryBranch LibraryBranch { get; set; }
    
    public long PublisherId { get; set; }

    [ForeignKey("PublisherId")]
    public Publisher Publisher { get; set; }

    public long LanguageId { get; set; }
    [ForeignKey("LanguageId")]
    public Language Language { get; set; }




}
