using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Domain.Entities.Events;
using Tahseen.Domain.Entities.Notifications;
using Tahseen.Service.DTOs.Events.Events;
using Tahseen.Service.DTOs.Notifications;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.INotificationServices;

namespace Tahseen.Service.Services.Notifications;

public class NotificationService : INotificationService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Notification> _repository;
    private readonly IRepository<User> _userRepository; 

    public NotificationService(
        IMapper mapper, 
        IRepository<Notification> repository,
        IRepository<User> userRepository)
    {
        this._mapper = mapper;
        this._repository = repository;
        _userRepository = userRepository;
    }

    public async Task<NotificationForResultDto> AddAsync(NotificationForCreationDto dto)
    {
        var user = await _userRepository.SelectAll().
               Where(u => u.Id == dto.UserId && u.IsDeleted == false).
               AsNoTracking().
               FirstOrDefaultAsync();

        if (user == null)
            throw new TahseenException(404, "User is not found");

        var notification = _mapper.Map<Notification>(dto);
        var result= await _repository.CreateAsync(notification);
        return _mapper.Map<NotificationForResultDto>(result);
    }

    public async Task<NotificationForResultDto> ModifyAsync(long id, NotificationForUpdateDto dto)
    {
        var notification = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();

        var user = await _userRepository.SelectAll().
               Where(u => u.Id == dto.UserId && u.IsDeleted == false).
               AsNoTracking().
               FirstOrDefaultAsync();

        if (user == null)
            throw new TahseenException(404, "User is not found");

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
        var AllData = this._repository
            .SelectAll()
            .Where(t => t.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        var result = this._mapper.Map<IEnumerable<NotificationForResultDto>>(AllData);
        foreach(var item in result)
        {
            item.NotificationStatus = item.NotificationStatus.ToString();
        }
        return result;
    }

    public async Task<NotificationForResultDto> RetrieveByIdAsync(long id)
    {
        var notification = await this._repository.SelectByIdAsync(id);
        if (notification is not null && !notification.IsDeleted) {
            var result = this._mapper.Map<NotificationForResultDto>(notification);
            result.NotificationStatus = result.NotificationStatus.ToString();
            return result;
        }
        throw new Exception("Notification not found");
    }
}