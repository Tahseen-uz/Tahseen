using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Feedbacks;
using Tahseen.Service.DTOs.Feedbacks.Surveys;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IFeedbackService;

namespace Tahseen.Service.Services.Feedbacks;

public class SurveyService:ISurveyService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Surveys> _repository;

    public SurveyService(IMapper mapper, IRepository<Surveys>repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<SurveyForResultDto> AddAsync(SurveyForCreationDto dto)
    {
        var mapped = _mapper.Map<Surveys> (dto);
        var result = await _repository.CreateAsync(mapped);
        return _mapper.Map<SurveyForResultDto>(result); 
    }

    public async Task<SurveyForResultDto> ModifyAsync(long id, SurveyForUpdateDto dto)
    {
        var survey = await _repository.SelectAll().Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefaultAsync();
        if (survey is null)
        {
            throw new TahseenException(404, "Survey doesn't found");
        }

        var modified = _mapper.Map(dto, survey);
        modified.UpdatedAt = DateTime.UtcNow;
        var result = _repository.UpdateAsync(modified);
        return _mapper.Map<SurveyForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
        => await _repository.DeleteAsync(id);

    public async Task<SurveyForResultDto?> RetrieveByIdAsync(long id)
    {
        var survey = await _repository.SelectByIdAsync(id);
        if (survey is null || survey.IsDeleted)
        {
            throw new TahseenException(404, "Survey doesn't found");
        }

        return _mapper.Map<SurveyForResultDto>(survey);
    }

    public async Task<IEnumerable<SurveyForResultDto>> RetrieveAllAsync()
    {
        var surveys = _repository.SelectAll().Where(x => !x.IsDeleted);
        return _mapper.Map<IEnumerable<SurveyForResultDto>>(surveys);
    }
}