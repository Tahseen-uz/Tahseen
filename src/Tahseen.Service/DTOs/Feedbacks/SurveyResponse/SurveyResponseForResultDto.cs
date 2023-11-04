using Tahseen.Service.DTOs.Feedbacks.Surveys;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Feedbacks.SurveyResponses;

public class SurveyResponseForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserForResultDto User { get; set; }
    public long SurveyId { get; set; }
    public SurveyForResultDto Surveys { get; set; }
    public DateTime SubmissionDate { get; set; }
}