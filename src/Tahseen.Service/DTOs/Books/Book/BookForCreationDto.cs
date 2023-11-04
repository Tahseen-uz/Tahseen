using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.Reservations;
using Tahseen.Domain.Enums;
using Tahseen.Service.DTOs.Books.CompletedBooks;
using Tahseen.Service.DTOs.Reservations;

namespace Tahseen.Service.DTOs.Books.Book;

public class BookForCreationDto
{
    public string Title { get; set; }
    public Language Language { get; set; }
    public long TotalCopies { get; set; }
    public long AvailableCopies { get; set; }
    public decimal Rating { get; set; }
    public long Reviews { get; set; }
    public BookFormat BookFormat { get; set; }
    public string ShelfLocation { get; set; }
    public BookCondition Condition { get; set; }
    public IFormFile BookImage { get; set; }
    public long AuthorId { get; set; }
    public long GenreId { get; set; }
    public long LibraryId { get; set; }
    public long PublisherId { get; set; }
    public string PrintedIn { get; set; }
    public long TotalPages { get; set; }
}