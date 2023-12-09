using System.Text.Json.Serialization;
using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Narrators;

namespace Tahseen.Domain.Entities.AudioBooks
{
    public class AudioBook : Auditable
    {
        public string Title { get; set; }
        public long AuthorId { get; set; }
        public Author Author { get; set; }
        public long NarratorId { get; set; }
        public Narrator Narrator { get; set; }
        public string Content { get; set; }
        public string Duration { get; set; }
        public long GenreId { get; set; }
        public Genre Genre { get; set; }
        public string Image { get; set; }
        public IEnumerable<AudioFile> AudioFiles { get; set; }
    }
}
