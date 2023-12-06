using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Languages;
using Tahseen.Service.DTOs.Books.Publishers;
using Tahseen.Service.DTOs.Languages;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.ILanguageServices;

namespace Tahseen.Service.Services.Languages;

public class LanguageService : ILanguageService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Language> _repository;


    public LanguageService(IMapper mapper, IRepository<Language> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<LanguageForResultDto> AddAsync(LanguageForCreationDto dto)
    {
        var language = await _repository.SelectAll()
            .Where(l => l.IsDeleted == false && l.Name == dto.Name)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (language != null)
            throw new TahseenException(409, "Language is already exist");

        var mapped = _mapper.Map<Language>(dto);


        var result = await _repository.CreateAsync(mapped);

        return _mapper.Map<LanguageForResultDto>(result);
    }

    public async Task<LanguageForResultDto> ModifyAsync(long Id, LanguageForUpdateDto dto)
    {
        var language = await _repository.SelectAll()
            .Where(l => l.IsDeleted == false && l.Id == Id)
            .FirstOrDefaultAsync();

        if (language is null)
            throw new TahseenException(404, "Language is not found");

        var mapped = _mapper.Map(dto, language);
        mapped.UpdatedAt = DateTime.UtcNow;

        var result = await _repository.UpdateAsync(mapped);

        return _mapper.Map<LanguageForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long Id)
    {
        var language = await _repository.SelectAll()
            .Where(l => l.IsDeleted == false && l.Id == Id)
            .FirstOrDefaultAsync();

        if (language is null)
            throw new TahseenException(404, "Language is not found");

        return await _repository.DeleteAsync(Id);
    }

    public async Task<IEnumerable<LanguageForResultDto>> RetrieveAllAsync()
    {
        var results = await _repository.SelectAll()
         .Where(t => !t.IsDeleted)
         .AsNoTracking()
         .ToListAsync();

        return _mapper.Map<IEnumerable<LanguageForResultDto>>(results);
    }

    public async Task<LanguageForResultDto> RetrieveByIdAsync(long Id)
    {
        var language = await _repository.SelectAll()
            .Where(l => l.IsDeleted == false && l.Id == Id)
            .FirstOrDefaultAsync();

        if (language is null)
            throw new TahseenException(404, "Language is not found");

        
        var result =  await _repository.SelectByIdAsync(Id);

        return _mapper.Map<LanguageForResultDto>(result);
    }
}
