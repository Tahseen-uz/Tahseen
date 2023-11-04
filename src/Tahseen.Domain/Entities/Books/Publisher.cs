using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Books;

public class Publisher:Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactInformation { get; set; }
    public string Image { get; set; }

    public IEnumerable<Book> Books { get; set;}
}
