using Tahseen.Service.DTOs.Feedbacks.Feedback;
using Tahseen.Service.DTOs.Feedbacks.SurveyResponses;

namespace Tahseen.Service.Interfaces.IFeedbackService;

public interface ISurveyResponseService
{
    public Task<SurveyResponseForResultDto> AddAsync(SurveyResponseForCreationDto dto);
    public Task<SurveyResponseForResultDto> ModifyAsync(long id, SurveyResponseForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<SurveyResponseForResultDto?> RetrieveByIdAsync(long id);
    public Task<IEnumerable<SurveyResponseForResultDto>> RetrieveAllAsync();
}