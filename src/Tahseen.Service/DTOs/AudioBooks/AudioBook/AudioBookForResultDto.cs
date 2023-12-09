using Tahseen.Service.DTOs.AudioBooks.AudioFile;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.Books.Genre;
using Tahseen.Service.DTOs.Narrators;

namespace Tahseen.Service.DTOs.AudioBooks.AudioBook;

public class AudioBookForResultDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public AuthorForResultDto Author { get; set; }
    public NarratorForResultDto Narrator { get; set; }
    public string Content { get; set; }
    public string Duration { get; set; }
    public GenreForResultDto Genre { get; set; }
    public string Image { get; set; }
    public IEnumerable<AudioFileForResultDto> AudioFiles { get; set; }


}
