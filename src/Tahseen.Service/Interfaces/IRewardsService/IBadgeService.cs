using Tahseen.Service.DTOs.Rewards.Badge;

namespace Tahseen.Service.Interfaces.IRewardsService
{
    public interface IBadgeService
    {
        public Task<BadgeForResultDto> AddAsync(BadgeForCreationDto dto);
        public Task<BadgeForResultDto> ModifyAsync(long Id, BadgeForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<BadgeForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<BadgeForResultDto>> RetrieveAllAsync();
    }
}
