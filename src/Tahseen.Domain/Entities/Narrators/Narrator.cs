using System.Text.Json.Serialization;
using Tahseen.Domain.Commons;
using Tahseen.Domain.Entities.AudioBooks;

namespace Tahseen.Domain.Entities.Narrators
{
    public class Narrator : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }

   //     [JsonIgnore]
        public IEnumerable<AudioBook> AudioBooks { get; set; }
    }
}
