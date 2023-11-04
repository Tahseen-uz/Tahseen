using Tahseen.Service.DTOs.Feedbacks.Surveys;
using Tahseen.Service.DTOs.Users.User;

namespace Tahseen.Service.DTOs.Feedbacks.SurveyResponses;

public class SurveyResponseForCreationDto
{
    public long UserId { get; set; }
    public UserForCreationDto User { get; set; }
    public long SurveyId { get; set; }
    public SurveyForCreationDto Survey { get; set; }
    public DateTime SubmissionDate { get; set; }
}