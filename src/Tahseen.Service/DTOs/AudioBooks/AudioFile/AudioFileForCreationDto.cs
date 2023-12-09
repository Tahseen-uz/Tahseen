using Microsoft.AspNetCore.Http;

namespace Tahseen.Service.DTOs.AudioBooks.AudioFile;

public class AudioFileForCreationDto
{
    public IFormFile FilePath { get; set; }
    public decimal FileSize { get; set; }
    public string Duration { get; set; }
    public int AudioBookId { get; set; }
}
