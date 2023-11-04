using Tahseen.Service.DTOs.Rewards.UserBadges;

namespace Tahseen.Service.Interfaces.IRewardsService
{
    public interface IUserBadgesService
    {
        public Task<UserBadgesForResultDto> AddAsync(UserBadgesForCreationDto dto);
        public Task<UserBadgesForResultDto> ModifyAsync(long Id, UserBadgesForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<UserBadgesForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<UserBadgesForResultDto>> RetrieveAllAsync();
    }
}
