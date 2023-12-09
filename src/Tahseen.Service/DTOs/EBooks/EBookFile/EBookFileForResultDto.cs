using Tahseen.Service.DTOs.EBooks.EBook;

namespace Tahseen.Service.DTOs.EBooks.EBookFile;

public class EBookFileForResultDto
{
    public long Id { get; set; }
    public long EBookId { get; set; }
    public EBookForResultDto EBook { get; set; }
    public string FilePath { get; set; }
    public decimal FileSize { get; set; }
}
