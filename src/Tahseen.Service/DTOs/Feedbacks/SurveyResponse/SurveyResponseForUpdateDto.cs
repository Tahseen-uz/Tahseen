using Tahseen.Service.DTOs.Feedbacks.Surveys;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Feedbacks.SurveyResponses;

public class SurveyResponseForUpdateDto
{
    public long UserId { get; set; }
    public UserForUpdateDto User { get; set; }
    public long SurveyId { get; set; }
   // public SurveyForUpdateDto Surveys { get; set; }
    public DateTime SubmissionDate { get; set; }
}