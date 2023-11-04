using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Books;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Service.DTOs.Feedbacks.News;
using Tahseen.Service.DTOs.FileUpload;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IFeedbackService;
using Tahseen.Service.Interfaces.IFileUploadService;

namespace Tahseen.Service.Services.Feedbacks;

public class NewsService:INewsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<News> _repository;
    private readonly IFileUploadService _fileUploadService;

    public NewsService(IMapper mapper, IRepository<News>repository, IFileUploadService fileUploadService)
    {
        _mapper = mapper;
        _repository = repository;
        _fileUploadService = fileUploadService;
    }
    public async Task<NewsForResultDto> AddAsync(NewsForCreationDto dto)
    {
        var data = await _repository.SelectAll()
            .Where(d => d.Title.ToLower() == dto.Title.ToLower() && d.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (data is not null)
            throw new TahseenException(409, "News is already exist");
        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "NewsAssets",
            FormFile = dto.Media,
        };

        var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = _mapper.Map<News> (dto);
        mapped.Media = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        var result = await _repository.CreateAsync(mapped);

        return _mapper.Map<NewsForResultDto>(result); 
    }

    public async Task<NewsForResultDto> ModifyAsync(long id, NewsForUpdateDto dto)
    {
        var news = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (news is null)
        {
            throw new TahseenException(404, "News doesn't found");
        }

        await _fileUploadService.FileDeleteAsync(news.Media);

        var FileUploadForCreation = new FileUploadForCreationDto
        {
            FolderPath = "NewsAssets",
            FormFile = dto.Media,
        };

        var FileResult = await _fileUploadService.FileUploadAsync(FileUploadForCreation);

        var mapped = _mapper.Map(dto, news);
        mapped.Media = Path.Combine("Assets", $"{FileResult.FolderPath}", FileResult.FileName);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = await _repository.UpdateAsync(mapped);
        return _mapper.Map<NewsForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var data = await _repository.SelectAll()
            .Where(d => d.Id == id && d.IsDeleted == false)
            .FirstOrDefaultAsync();
        if (data is null)
            throw new TahseenException(404, "News is not found");

        await _fileUploadService.FileDeleteAsync (data.Media);

        return await _repository.DeleteAsync(id);
    }

    public async Task<NewsForResultDto?> RetrieveByIdAsync(long id)
    {
        var news = await _repository.SelectByIdAsync(id);
        if (news is null || news.IsDeleted)
        {
            throw new TahseenException(404, "News doesn't found");
        }
        news.Media = $"https://localhost:7020/{news.Media.Replace('\\', '/').TrimStart('/')}";
        return _mapper.Map<NewsForResultDto>(news);
    }

    public async Task<IEnumerable<NewsForResultDto>> RetrieveAllAsync()
    {
        var news = _repository.SelectAll().Where(x => !x.IsDeleted);
        foreach (var result in news)
        {
            result.Media = $"https://localhost:7020/{result.Media.Replace('\\', '/').TrimStart('/')}";
        }
        return _mapper.Map<IEnumerable<NewsForResultDto>>(news);
    }
}