using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.Genre;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.DTOs.Librarians;

namespace Tahseen.Service.DTOs.Books.Book;

public class BookForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public Language Language { get; set; }
    public long TotalCopies { get; set; }
    public long AvailableCopies { get; set; }
    public decimal Rating { get; set; }
    public long Reviews { get; set; }
    public BookFormat BookFormat { get; set; }
    public string ShelfLocation { get; set; }
    public BookCondition Condition { get; set; }
    public string BookImage { get; set; }
    public long AuthorId { get; set; }
    public long GenreId { get; set; }
    public long LibraryId { get; set; }
    public long PublisherId { get; set; }
    public string PrintedIn { get; set; }
    public long TotalPages { get; set; }
}