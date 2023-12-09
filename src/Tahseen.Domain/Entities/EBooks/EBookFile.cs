using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;

namespace Tahseen.Domain.Entities.EBooks
{
    public class EBookFile : Auditable
    {
        public long EBookId { get; set; }
        public EBook EBook { get; set; }
        public string FilePath { get; set; }
        public decimal FileSize { get; set; } 

    }
}
