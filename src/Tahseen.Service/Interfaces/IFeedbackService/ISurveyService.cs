using Tahseen.Service.DTOs.Feedbacks.Surveys;

namespace Tahseen.Service.Interfaces.IFeedbackService;

public interface ISurveyService
{
    public Task<SurveyForResultDto> AddAsync(SurveyForCreationDto dto);
    public Task<SurveyForResultDto> ModifyAsync(long id, SurveyForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<SurveyForResultDto?> RetrieveByIdAsync(long id);
    public Task<IEnumerable<SurveyForResultDto>> RetrieveAllAsync();
}