using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Users.UserSettings;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.Users
{
    public class UserSettingsService : IUserSettingService
    {
        private readonly IRepository<UserSettings> _repository;
        private readonly IMapper _mapper;
        public UserSettingsService(IRepository<UserSettings> Repository, IMapper Mapper)
        {
            this._mapper = Mapper;
            this._repository = Repository;
        }
        public async Task<UserSettingsForResultDto> AddAsync(UserSettingsForCreationDto dto)
        {
            var existingUser = await this._repository.SelectAll()
                .Where(u => u.UserId == dto.UserId && u.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (existingUser == null)
            {
                var data = this._mapper.Map<UserSettings>(dto);
                var result = await this._repository.CreateAsync(data);
                return this._mapper.Map<UserSettingsForResultDto>(result);
            }
            // If no user with the specified UserId is found, you can throw a more descriptive exception.
            throw new TahseenException(404, "User not found for the provided UserId.");

        }


        public async Task<UserSettingsForResultDto> ModifyAsync(long Id, UserSettingsForUpdateDto dto)
        {
            var Data = await this._repository
                .SelectAll()
                .Where(e => e.Id == Id && e.IsDeleted == false)
                .FirstOrDefaultAsync();
            if(Data != null)
            {
                var MappedData = this._mapper.Map(dto, Data);
                MappedData.UpdatedAt = DateTime.UtcNow;
                var result = await this._repository.UpdateAsync(MappedData);
                return this._mapper.Map<UserSettingsForResultDto>(result);
            }
            throw new TahseenException(404, "User is not found");
        }

        public async Task<bool> RemoveAsync(long Id)
        {
            return await this._repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<UserSettingsForResultDto>> RetrieveAllAsync()
        {
            var AllData = await this._repository
                .SelectAll()
                .Where(u => u.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            var result = this._mapper.Map<IEnumerable<UserSettingsForResultDto>>(AllData);    
            foreach(var item in result)
            {
                item.LanguagePreference = item.LanguagePreference.ToString();
                item.ThemePreference = item.ThemePreference.ToString();
                item.NotificationPreference = item.NotificationPreference.ToString();   
            }
            return result;
        }

        public async Task<UserSettingsForResultDto> RetrieveByIdAsync(long Id)
        {
            var Data = await this._repository.SelectAll()
                .Where(u => u.Id == Id && !u.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (Data is null)
                throw new TahseenException(404, "UserSetting is not found");
            var result = this._mapper.Map<UserSettingsForResultDto>(Data);
            result.NotificationPreference = result.NotificationPreference.ToString();
            result.ThemePreference = result.ThemePreference.ToString();
            result.LanguagePreference = result.LanguagePreference.ToString();
            return result;
        }
    }
}
