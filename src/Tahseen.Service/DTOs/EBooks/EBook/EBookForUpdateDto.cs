using Microsoft.AspNetCore.Http;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Service.DTOs.EBooks.EBook;

public class EBookForUpdateDto
{
    public string Title { get; set; }
    public long AuthorId { get; set; }
    public string Description { get; set; }
    public long GenreId { get; set; }
    public IFormFile Image { get; set; }

}
