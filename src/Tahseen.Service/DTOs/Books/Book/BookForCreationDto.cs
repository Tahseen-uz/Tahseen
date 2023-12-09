using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
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
    public long TotalCopies { get; set; }
    public long AvailableCopies { get; set; }
    public string Content { get; set; }
    public string ShelfLocation { get; set; }
    public IFormFile BookImage { get; set; }
    public long TotalPages { get; set; }
    public long AuthorId { get; set; }
    public long GenreId { get; set; }
    public long LibraryId { get; set; }
    public long PublisherId { get; set; }
    public long LanguageId { get; set; }

}