using Tahseen.Domain.Enums;

namespace Tahseen.Service.DTOs.Feedbacks.Surveys;

public class SurveyForUpdateDto
{
    public string SurveyTitle { get; set; }
    public string SurveyDescription { get; set; }
    public string Question { get; set; }
    public DateTime EndDate { get; set; }
    public SurveyStatus Status { get; set; }
}