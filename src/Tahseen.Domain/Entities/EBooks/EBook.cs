using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Domain.Entities.EBooks
{
    public class EBook : Auditable
    {
        public string Title { get; set; }
        public long AuthorId { get; set; }
        public Author Author { get; set; }
        public string Description { get; set; }
        public long GenreId { get; set; }
        public Genre Genre { get; set; }
        public string Image {  get; set; }
    }
}
