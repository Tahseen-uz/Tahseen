using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Books.Book;

public class BookForUpdateDto
{
    public string Title { get; set; }
    public long TotalCopies { get; set; }
    public string Content { get; set; }
    public string ShelfLocation { get; set; }
    public IFormFile BookImage { get; set; }
    public long TotalPages { get; set; }
}