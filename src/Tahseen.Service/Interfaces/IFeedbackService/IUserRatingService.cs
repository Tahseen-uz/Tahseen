using Tahseen.Service.DTOs.Feedbacks.UserRatings;

namespace Tahseen.Service.Interfaces.IFeedbackService;

public interface IUserRatingService
{
    public Task<UserRatingForResultDto> AddAsync(UserRatingForCreationDto dto);
    public Task<UserRatingForResultDto> ModifyAsync(long id, UserRatingForUpdateDto dto);
    public Task<bool> RemoveAsync(long Id);
    public Task<UserRatingForResultDto> RetrieveByIdAsync(long id);
    public Task<UserRatingForResultDto> RetrieveByUserId(long userId);
    public Task<IEnumerable<UserRatingForResultDto>> RetrieveAllAsync();
}
