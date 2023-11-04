using Tahseen.Service.DTOs.Feedbacks.UserMessages;

namespace Tahseen.Service.Interfaces.IFeedbackService;

public interface IUserMessageService
{
    public Task<UserMessageForResultDto> AddAsync(UserMessageForCreationDto dto);
    public Task<UserMessageForResultDto> ModifyAsync(long id,UserMessageForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<UserMessageForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<UserMessageForResultDto>> RetrieveAllAsync();
}
