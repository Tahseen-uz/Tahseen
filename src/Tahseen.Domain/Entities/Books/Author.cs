using Tahseen.Domain.Commons;
using Tahseen.Domain.Enums;

namespace Tahseen.Domain.Entities.Books;

public class Author:Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set;}
    public string AuthorImage { get; set; }
    public string Biography { get; set; }
    public Nationality Nationality { get; set; }
    public IEnumerable<Book> Books { get; set; }
}
