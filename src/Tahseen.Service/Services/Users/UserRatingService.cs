using AutoMapper;
using Tahseen.Data.IRepositories;
using Tahseen.Service.Exceptions;
using Tahseen.Service.DTOs.Feedbacks.UserRatings;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Domain.Entities.Feedbacks;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.Repositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities;

namespace Tahseen.Service.Services.Users;

public class UserRatingService : IUserRatingService
{
    private readonly IMapper mapper;
    private readonly IRepository<UserRatings> repository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<CompletedBooks> _CompletedBooksRepository;
    public UserRatingService(IMapper mapper, IRepository<UserRatings> repository, IRepository<User> userRepository, IRepository<CompletedBooks> completedBooksRepository)
    {
        this.mapper = mapper;
        this.repository = repository;
        this._userRepository = userRepository;
        _CompletedBooksRepository = completedBooksRepository;
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
            .FirstOrDefaultAsync();

        if (existingRating != null)
        {
            throw new TahseenException(409, "This User Rating already exists");
        }

        var mappedUserRating = mapper.Map<UserRatings>(dto);
        var userRating = await repository.CreateAsync(mappedUserRating);
        return mapper.Map<UserRatingForResultDto>(userRating);
    }

    public async Task<UserRatingForResultDto> ModifyAsync(long Id, UserRatingForUpdateDto dto)
    {
        var user = await _userRepository.SelectAll()
            .Where(u => u.IsDeleted == false && u.Id == Id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (user is null)
            throw new TahseenException(404, "User is not found");

        var userRating = await repository.SelectAll().Where(e => e.UserId == Id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (userRating == null)
            throw new TahseenException(404, "UserRating not found");

        var mappedUserRating = mapper.Map(dto, userRating);
        var result = await repository.UpdateAsync(mappedUserRating);

        return mapper.Map<UserRatingForResultDto>(result);
    }



    public async Task<bool> RemoveAsync(long Id)
    {
        var Rating = this.repository.SelectAll().Where(e => e.Id == Id && e.IsDeleted == false).AsNoTracking().FirstOrDefaultAsync();
        if (Rating != null)
        {
            return await this.repository.DeleteAsync(Id);
        }

        throw new TahseenException(404, "Not Found");
    }


    public async Task<IEnumerable<UserRatingForResultDto>> RetrieveAllAsync()
    {
        var allUsersRatings = await repository.SelectAll()
            .Where(t => t.IsDeleted == false)
            .AsNoTracking()
            .ToListAsync();

        var allUsersCompletedBooks = await _CompletedBooksRepository
            .SelectAll()
            .GroupBy(c => c.UserId)
            .Select(group => new
            {
                UserId = group.Key,
                BooksCompletedCount = group.Count()
            })
            .OrderByDescending(x => x.BooksCompletedCount)
            .ToListAsync();

        var userRatingResultDtos = allUsersRatings.Select(userRatingEntity =>
        {
            var booksCompletedCount = allUsersCompletedBooks
                .FirstOrDefault(x => x.UserId == userRatingEntity.UserId)?
                .BooksCompletedCount ?? 0;

            var userPosition = allUsersCompletedBooks.FindIndex(x => x.UserId == userRatingEntity.UserId) + 1;

            var userRatingResultDto = mapper.Map<UserRatingForResultDto>(userRatingEntity);
            userRatingResultDto.BooksCompleted = booksCompletedCount;
            userRatingResultDto.Rating = userPosition;

            return userRatingResultDto;
        });

        if (!userRatingResultDtos.Any())
        {
            throw new TahseenException(404, "Not found");
        }

        return userRatingResultDtos;
    }



    public async Task<UserRatingForResultDto> RetrieveByUserIdAsync(long userId)
    {
        var userRatingEntity = await repository.SelectAll()
            .Where(t => t.UserId == userId && t.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (userRatingEntity == null)
            throw new TahseenException(404, "UserRating not found");

        if(userRatingEntity.BooksCompleted == 0)
        {
            return this.mapper.Map<UserRatingForResultDto>(userRatingEntity);
        }

        var booksCompletedCount = await _CompletedBooksRepository
           .SelectAll()
           .Where(c => c.UserId == userId)
           .CountAsync();

        userRatingEntity.BooksCompleted = booksCompletedCount;

        // Find the user's position among all users based on completed books count
        var allUsersCompletedBooks = await _CompletedBooksRepository
            .SelectAll()
            .GroupBy(c => c.UserId)
            .Select(group => new
            {
                UserId = group.Key,
                BooksCompletedCount = group.Count()
            })
            .OrderByDescending(x => x.BooksCompletedCount)
            .ToListAsync();

        // Find the user's position among all users based on completed books count
        var userPosition = allUsersCompletedBooks.FindIndex(x => x.UserId == userId) + 1;

        // Populate the user's position without scaling to a 5-point rating system
        var userRatingResultDto = mapper.Map<UserRatingForResultDto>(userRatingEntity);
        userRatingResultDto.Rating = userPosition;

        return userRatingResultDto;
    }

}
