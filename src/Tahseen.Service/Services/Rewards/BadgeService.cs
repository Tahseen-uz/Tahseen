using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Reservations;
using Tahseen.Domain.Entities.Rewards;
using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.DTOs.Reservations;
using Tahseen.Service.DTOs.Rewards.Badge;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IFileUploadService;
using Tahseen.Service.Interfaces.IRewardsService;

namespace Tahseen.Service.Services.Rewards;

public class BadgeService : IBadgeService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Badge> _repository;
    private readonly IFileUploadService _fileUploadService;
    public BadgeService(IMapper mapper, IRepository<Badge> repository, IFileUploadService fileUploadService)
    {
        this._mapper = mapper;
        this._repository = repository;
        this._fileUploadService = fileUploadService;
    }

    public async Task<BadgeForResultDto> AddAsync(BadgeForCreationDto dto)
    {
        var Check = this._repository.SelectAll().Where(b => b.Name == dto.Name).FirstOrDefault();
        if (Check != null)
        {
            throw new TahseenException(409, "This Badge is exist");
        }

        var badge = _mapper.Map<Badge>(dto);
        if(dto.ImageUrl != null)
        {
            var FileUploadForCreation = new FileUploadForCreationDto
            {
                FolderPath = "BadgeAssets",
                FormFile = dto.ImageUrl
            };
            var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

            badge.ImageUrl = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        }

        var result= await _repository.CreateAsync(badge);
        return _mapper.Map<BadgeForResultDto>(result);
    }

    public async Task<BadgeForResultDto> ModifyAsync(long id, BadgeForUpdateDto dto)
    {
        var badge = await _repository.SelectAll().Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (badge is not null)
        {
            if(dto != null && dto.ImageUrl != null)
            {
                if(dto.ImageUrl != null)
                {
                    await _fileUploadService.FileDeleteAsync(badge.ImageUrl);
                }
                var FileUploadForCreation = new FileUploadForCreationDto
                {
                    FolderPath = "BadgeAssets",
                    FormFile = dto.ImageUrl
                };

                var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

                badge.ImageUrl = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
                badge.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : badge.Name;
            }
            if (dto != null && dto.ImageUrl == null)
            {
                badge.ImageUrl = badge.ImageUrl;
                badge.Name = !string.IsNullOrEmpty(dto.Name) ? dto.Name : badge.Name;
            }

            badge.UpdatedAt = DateTime.UtcNow;
            var result = await _repository.UpdateAsync(badge);
            return _mapper.Map<BadgeForResultDto>(result);
        }
        throw new Exception("Badge not found");
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var badge = await _repository.SelectAll()
            .Where(b => b.Id == id && b.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (badge is null)
            throw new TahseenException(404,"User is not found");
        if(badge.ImageUrl != null) 
            await _fileUploadService.FileDeleteAsync(badge.ImageUrl);
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BadgeForResultDto>> RetrieveAllAsync()
    {
        var AllData = this._repository.SelectAll().Where(t => t.IsDeleted == false);
      
        return _mapper.Map<IEnumerable<BadgeForResultDto>>(AllData);
    }

    public async Task<BadgeForResultDto> RetrieveByIdAsync(long id)
    {
        var badge = await _repository.SelectByIdAsync(id);
        if (badge is not null && !badge.IsDeleted)
        {
            return _mapper.Map<BadgeForResultDto>(badge);
        }
        
        throw new Exception("Badge not found");
    }
}