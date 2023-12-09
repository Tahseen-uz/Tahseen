using AutoMapper;
using Tahseen.Domain.Entities;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.Interfaces.IFeedbackService;

namespace Tahseen.Service.Services.Users;

public class UserRatingService : IUserRatingService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRatings> repository;
    public UserRatingService(
        IMapper mapper, 
        IRepository<User> userRepository,
        IRepository<UserRatings> repository) 
    {
        this.mapper = mapper;
        this.repository = repository;
        this._userRepository = userRepository;
    }
    public async Task<UserRatingForResultDto> AddAsync(UserRatingForCreationDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var existingRating = await this.repository.SelectAll()
            .Where(u => u.UserId == dto.UserId && u.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (existingRating != null)
            throw new TahseenException(409, "This User Rating already exists");

        var mappedUserRating = mapper.Map<UserRatings>(dto);
        var userRating = await repository.CreateAsync(mappedUserRating);
        return mapper.Map<UserRatingForResultDto>(userRating);
    }

    // o'zgartiriladi
    // yoge aldameng ezizbe
    public async Task<UserRatingForResultDto> ModifyAsync(long Id, UserRatingForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var userRating = await repository
            .SelectAll()
            .Where(e => e.Id == Id && e.IsDeleted == false)
            .FirstOrDefaultAsync(); 

        if (userRating == null)
            throw new TahseenException(404, "UserRating not found");

        var mappedUserRating = mapper.Map(dto, userRating);
        var result = await repository.UpdateAsync(mappedUserRating);

        return mapper.Map<UserRatingForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long Id)
    {
        var data = await this.repository
            .SelectAll()
            .Where(u => u.Id == Id && !u.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (data is null)
            throw new TahseenException(404, "User Setting is not found");
        return await repository.DeleteAsync(Id);
    }

    public async Task<IEnumerable<UserRatingForResultDto>> RetrieveAllAsync()
    {
        var results = await repository.SelectAll()
            .Where(t => !t.IsDeleted)
            .AsNoTracking()
            .ToListAsync();
        return mapper.Map<IEnumerable<UserRatingForResultDto>>(results);
    }

    public async Task<UserRatingForResultDto> RetrieveByIdAsync(long id)
    {
        var result = await repository.SelectByIdAsync(id);
        if (result == null && result.IsDeleted)
            throw new TahseenException(404, "UserRating not found");

        return mapper.Map<UserRatingForResultDto>(result);
    }

    public async Task<UserRatingForResultDto> RetrieveByUserIdAsync(long userId)
    {
        var result = await repository.SelectAll()
            .Where(t => t.UserId == userId && !t.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (result == null)
            throw new TahseenException(404, "UserRating not found");

        return mapper.Map<UserRatingForResultDto>(result);
    }
}
