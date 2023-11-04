using Tahseen.Domain.Commons;

namespace Tahseen.Domain.Entities.Feedbacks;

public class SurveyResponses:Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long SurveyId { get; set; }
    public Surveys Surveys { get; set; }
    public DateTime SubmissionDate { get; set; }
}