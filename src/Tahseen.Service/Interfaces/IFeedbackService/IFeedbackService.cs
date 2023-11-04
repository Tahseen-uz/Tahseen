using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.DTOs.Feedbacks.Feedback;

namespace Tahseen.Service.Interfaces.IFeedbackService;

public interface IFeedbackService
{
    public Task<FeedbackForResultDto> AddAsync(FeedbackForCreationDto dto);
    public Task<FeedbackForResultDto> ModifyAsync(long id, FeedbackForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<FeedbackForResultDto?> RetrieveByIdAsync(long id);
    public Task<IEnumerable<FeedbackForResultDto>> RetrieveAllAsync();
}