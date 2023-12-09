using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.EBooks.EBookFile;

public class EBookFileForCreationDto
{
    public long EBookId { get; set; }
    public IFormFile FilePath { get; set; }
    public decimal FileSize { get; set; }
}
