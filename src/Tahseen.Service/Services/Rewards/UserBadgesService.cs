using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Rewards;
using Tahseen.Service.DTOs.Rewards.Badge;
using Tahseen.Service.DTOs.Rewards.UserBadges;
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
        var userBadges = _mapper.Map<UserBadges>(dto);
        var result= await _repository.CreateAsync(userBadges);
        return _mapper.Map<UserBadgesForResultDto>(result);
    }

    public async Task<UserBadgesForResultDto> ModifyAsync(long id, UserBadgesForUpdateDto dto)
    {
        var userBadges = await _repository.SelectByIdAsync(id);
        if (userBadges is not null && !userBadges.IsDeleted)
        {
            var mappedUserBadges = _mapper.Map<UserBadges>(dto);
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

    public ICollection<UserBadgesForResultDto> RetrieveAll()
    {
        var AllData = this._repository.SelectAll().Where(t => t.IsDeleted == false);
        return _mapper.Map<ICollection<UserBadgesForResultDto>>(AllData);
    }

    public async Task<UserBadgesForResultDto> RetrieveByIdAsync(long id)
    {
        var userBadges = await _repository.SelectByIdAsync(id);
        if (userBadges is not null && !userBadges.IsDeleted)
            return _mapper.Map<UserBadgesForResultDto>(userBadges);
        
        throw new Exception("UserBadges not found");
    }

   
}