using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Users.UserProgressTracking;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class UserProgressTrackingService : IUserProgressTrackingService
    {
        private readonly IRepository<UserProgressTracking> _repository;
        private readonly IMapper _mapper;
        public async Task<UserProgressTrackingForResultDto> AddAsync(UserProgressTrackingForCreationDto dto)
        {
            var result = this._mapper.Map<UserProgressTracking>(dto);
            var CreatedData = await _repository.CreateAsync(result);
            return this._mapper.Map<UserProgressTrackingForResultDto>(CreatedData);
        }

        public async Task<UserProgressTrackingForResultDto> Modify(long Id, UserProgressTrackingForUpdateDto dto)
        {
            var Data = await _repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).FirstOrDefaultAsync();
            if (Data != null )
            {
                var MappedData = this._mapper.Map(dto, Data);
                MappedData.UpdatedAt = DateTime.UtcNow;
                var result = await this._repository.UpdateAsync(MappedData);
                return this._mapper.Map<UserProgressTrackingForResultDto>(result);
            }
            throw new TahseenException(404, "NotFound");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserProgressTrackingForResultDto>> RetrieveAllAsync()
        {
            var AllData = this._repository.SelectAll();
            return this._mapper.Map<IEnumerable<UserProgressTrackingForResultDto>>(AllData);
        }

        public async Task<UserProgressTrackingForResultDto> RetrieveByIdAsync(long Id)
        {
            var result = await this._repository.SelectByIdAsync(Id);
            return this._mapper.Map<UserProgressTrackingForResultDto>(result);
        }
    }
}
