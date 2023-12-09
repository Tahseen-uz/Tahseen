using Tahseen.Domain.Entities.Books;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.Genre;

namespace Tahseen.Service.DTOs.EBooks.EBook;

public class EBookForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public AuthorForResultDto Author { get; set; }
    public string Description { get; set; }
    public GenreForResultDto Genre { get; set; }
    public string Image { get; set; }

}
