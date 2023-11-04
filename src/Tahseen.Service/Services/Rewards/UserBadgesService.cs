using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Rewards;
using Tahseen.Service.DTOs.Rewards.Badge;
using Tahseen.Service.DTOs.Rewards.UserBadges;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IRewardsService;

namespace Tahseen.Service.Services.Rewards;

public class UserBadgesService : IUserBadgesService
{
    private readonly IMapper _mapper;
    private readonly IRepository<UserBadges> _repository;

    public UserBadgesService(IMapper mapper, IRepository<UserBadges> repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<UserBadgesForResultDto> AddAsync(UserBadgesForCreationDto dto)
    {
        var Check = this._repository.SelectAll().Where(u => u.BadgeId == dto.BadgeId && u.UserId == dto.BadgeId && u.IsDeleted == false).FirstOrDefaultAsync();
        if (Check != null)
        {
            throw new TahseenException(409, "This Badge is exist");
        }
        var userBadges = _mapper.Map<UserBadges>(dto);
        var result= await _repository.CreateAsync(userBadges);
        return _mapper.Map<UserBadgesForResultDto>(result);
    }

    public async Task<UserBadgesForResultDto> ModifyAsync(long id, UserBadgesForUpdateDto dto)
    {
        var userBadges = await _repository.SelectAll().Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (userBadges is not null)
        {
            var mappedUserBadges = _mapper.Map(dto, userBadges);
            mappedUserBadges.UpdatedAt = DateTime.UtcNow;
            var result = await _repository.UpdateAsync(mappedUserBadges);
            return _mapper.Map<UserBadgesForResultDto>(result);
        }
        throw new Exception("UserBadges not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<UserBadgesForResultDto>> RetrieveAllAsync()
    {
        var AllData = this._repository.SelectAll().Where(t => t.IsDeleted == false);
        return _mapper.Map<IEnumerable<UserBadgesForResultDto>>(AllData);
    }

    public async Task<UserBadgesForResultDto> RetrieveByIdAsync(long id)
    {
        var userBadges = await _repository.SelectByIdAsync(id);
        if (userBadges is not null && !userBadges.IsDeleted)
            return _mapper.Map<UserBadgesForResultDto>(userBadges);
        
        throw new Exception("UserBadges not found");
    }

   
}