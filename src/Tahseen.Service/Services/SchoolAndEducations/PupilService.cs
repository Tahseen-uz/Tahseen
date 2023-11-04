using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.ISchoolAndEducation;

namespace Tahseen.Service.Services.SchoolAndEducations;

public class PupilService:IPupilService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Pupil> _repository;

    public PupilService(IMapper mapper, IRepository<Pupil>repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<PupilForResultDto> AddAsync(PupilForCreationDto dto)
    {
        var Check = this._repository.SelectAll().Where(p => p.FirstName == dto.FirstName && p.LastName == dto.LastName && p.LibraryBranchId == dto.LibraryBranchId && p.Grade == dto.Grade && p.IsDeleted == false);
        if(Check != null)
        {
            throw new TahseenException(409, "This pupil is exist");
        }
        var mapped = _mapper.Map<Pupil> (dto);
        var result = await _repository.CreateAsync(mapped);
        return _mapper.Map<PupilForResultDto>(result);
    }

    public async Task<PupilForResultDto> ModifyAsync(long id, PupilForUpdateDto dto)
    {
        var searched = await _repository.SelectAll().Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (searched is null)
        {
            throw new TahseenException(404,"Pupil not found");
        }

        var updated = _mapper.Map(dto, searched);
        updated.UpdatedAt = DateTime.UtcNow;
        var result = _repository.UpdateAsync(updated);
        return _mapper.Map<PupilForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
        => await _repository.DeleteAsync(id);

    public async Task<PupilForResultDto> RetrieveByIdAsync(long id)
    {
        var searched =await _repository.SelectByIdAsync(id);
        if (searched is null || searched.IsDeleted)
        {
           throw new TahseenException(404,"Pupil not found");
        }

        return _mapper.Map<PupilForResultDto>(searched);
    }

    public async Task<IEnumerable<PupilForResultDto>> RetrieveAllAsync()
    {
        var pupils = _repository.SelectAll().Where(x => !x.IsDeleted);
        return _mapper.Map<IEnumerable<PupilForResultDto>>(pupils);
    }
}