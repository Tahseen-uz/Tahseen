using Tahseen.Service.DTOs.Feedbacks.Feedback;
using Tahseen.Service.DTOs.Feedbacks.News;

namespace Tahseen.Service.Interfaces.IFeedbackService;

public interface INewsService
{
    public Task<NewsForResultDto> AddAsync(NewsForCreationDto dto);
    public Task<NewsForResultDto> ModifyAsync(long id, NewsForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<NewsForResultDto?> RetrieveByIdAsync(long id);
    public Task<IEnumerable<NewsForResultDto>> RetrieveAllAsync();
}