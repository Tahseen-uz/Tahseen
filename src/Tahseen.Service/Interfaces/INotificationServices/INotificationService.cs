using Tahseen.Service.DTOs.Notifications;

namespace Tahseen.Service.Interfaces.INotificationServices
{
    public interface INotificationService
    {
        public Task<NotificationForResultDto> AddAsync(NotificationForCreationDto dto);
        public Task<NotificationForResultDto> ModifyAsync(long Id, NotificationForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<NotificationForResultDto> RetrieveByIdAsync(long Id);
        public Task<IEnumerable<NotificationForResultDto>> RetrieveAllAsync();
    }
}
