using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Service.DTOs.AudioBooks.AudioBook;

public class AudioBookForCreationDto
{
    public string Title { get; set; }
    public long AuthorId { get; set; }
    public long NarratorId { get; set; }
    public string Content { get; set; }
    public string Duration { get; set; }
    public long GenreId { get; set; }
    public IFormFile Image { get; set; }
}
