using Tahseen.Domain.Entities.Feedbacks;
namespace Tahseen.Domain.Commons
{
    public abstract class Auditable
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set;}
    //public IEnumerable<Feedback> Feedbacks { get; set; }
    }
}
