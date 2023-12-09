using Tahseen.Domain.Entities.AudioBooks;
using Tahseen.Service.DTOs.AudioBooks.AudioBook;

namespace Tahseen.Service.DTOs.AudioBooks.AudioFile;

public class AudioFileForResultDto
{
    public long Id { get; set; }
    public string FilePath { get; set; }
    public decimal FileSize { get; set; }
    public string Duration { get; set; }
    public long AudioBookId { get; set; }
}
