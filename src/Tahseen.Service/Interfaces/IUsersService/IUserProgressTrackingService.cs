﻿using Tahseen.Service.DTOs.Users.UserProgressTracking;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IUserProgressTrackingService
    {
        public Task<UserProgressTrackingForResultDto> AddAsync(UserProgressTrackingForCreationDto dto);
        public Task<UserProgressTrackingForResultDto> Modify(long Id, UserProgressTrackingForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public ICollection<UserProgressTrackingForResultDto> RetrieveAll();
        public Task<UserProgressTrackingForResultDto> RetrieveByIdAsync(long Id);

    }
}