using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Events;
using Tahseen.Domain.Entities.Notifications;
using Tahseen.Service.DTOs.Events.Events;
using Tahseen.Service.DTOs.Notifications;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Interfaces.INotificationServices;

namespace Tahseen.Service.Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Notification> _repository;

    public NotificationService(IMapper mapper, IRepository<Notification> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<NotificationForResultDto> AddAsync(NotificationForCreationDto dto)
    {
        var notification = _mapper.Map<Notification>(dto);
        var result= await _repository.CreateAsync(notification);
        return _mapper.Map<NotificationForResultDto>(result);
    }

    public async Task<NotificationForResultDto> ModifyAsync(long id, NotificationForUpdateDto dto)
    {
        var notification = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (notification is not null)
        {
            var mappedNotification = _mapper.Map(dto, notification);
            var result = await _repository.UpdateAsync(mappedNotification);
            result.UpdatedAt = DateTime.UtcNow;
            return _mapper.Map<NotificationForResultDto>(result);
        }
        throw new Exception("Notification not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<NotificationForResultDto>> RetrieveAllAsync()
    {
        var AllData = this._repository.SelectAll().Where(t => t.IsDeleted == false);
        return _mapper.Map<IEnumerable<NotificationForResultDto>>(AllData);
    }

    public async Task<NotificationForResultDto> RetrieveByIdAsync(long id)
    {
        var notification = await _repository.SelectByIdAsync(id);
        if (notification is not null && !notification.IsDeleted)
            return _mapper.Map<NotificationForResultDto>(notification);
        
        throw new Exception("Notification not found");
    }
}