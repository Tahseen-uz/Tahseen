using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Feedbacks;

public class News : Auditable
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Media { get; set; }
    public string Author { get; set; }
    public DateTime PublicationDate { get; set; }
}