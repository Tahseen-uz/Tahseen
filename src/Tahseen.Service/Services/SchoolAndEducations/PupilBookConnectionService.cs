using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.Library;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.ISchoolAndEducation;

namespace Tahseen.Service.Services.SchoolAndEducations;

public class PupilBookConnectionService:IPupilBookConnectionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<PupilBookConnection> _repository;
    private readonly IRepository<Pupil> _pupilRepository;
    private readonly IRepository<SchoolBook> _schoolBookRepository;
    private readonly IRepository<LibraryBranch> _libraryRepository;
    public PupilBookConnectionService(
        IMapper mapper, 
        IRepository<PupilBookConnection> repository,
        IRepository<Pupil> pupilRepository,
        IRepository<SchoolBook> schoolBookRepository,
        IRepository<LibraryBranch> libraryRepository
        )
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<PupilBookConnectionForResultDto> AddAsync(PupilBookConnectionForCreationDto dto)
    {
        var Check = this._repository.SelectAll().Where(p => p.PupilId == dto.PupilId && p.SchoolBookId == dto.SchoolBookId && p.LibraryBranchId == dto.PupilId && p.IsDeleted == false);

        var pupil = await _pupilRepository.SelectAll()
            .Where(p => p.Id == dto.PupilId && p.IsDeleted == false)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (pupil is null)
            throw new TahseenException(404, "Pupil is not found");

        var schoolBook = await _schoolBookRepository.SelectAll()
            .Where(s => s.IsDeleted == false && s.Id == dto.SchoolBookId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (schoolBook is null)
            throw new TahseenException(404, "SchoolBook is not found");

        var library = await _libraryRepository.SelectAll()
            .Where(l => l.IsDeleted == false && l.Id == dto.LibraryBranchId)
            .AsNoTracking().
            FirstOrDefaultAsync();

        if (library is null)
            throw new TahseenException(404, "LibraryBranch is not found");

        if(Check != null)
        {
            throw new TahseenException(409, "This pupil has this book");
        }
        var mapped = _mapper.Map<PupilBookConnection>(dto);
        var result = _repository.CreateAsync(mapped);
        return _mapper.Map<PupilBookConnectionForResultDto>(result);
    }

    public async Task<PupilBookConnectionForResultDto> ModifyAsync(long id, PupilBookConnectionForUpdateDto dto)
    {
        var mapped = await _repository.SelectAll().Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (mapped is null)
        {
            throw new TahseenException(404, "PupilBookConnection is not found");
        }

        var pupil = await _pupilRepository.SelectAll()
           .Where(p => p.Id == dto.PupilId && p.IsDeleted == false)
           .AsNoTracking()
           .FirstOrDefaultAsync();

        if (pupil is null)
            throw new TahseenException(404, "Pupil is not found");

        var schoolBook = await _schoolBookRepository.SelectAll()
            .Where(s => s.IsDeleted == false && s.Id == dto.SchoolBookId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (schoolBook is null)
            throw new TahseenException(404, "SchoolBook is not found");

        var library = await _libraryRepository.SelectAll()
            .Where(l => l.IsDeleted == false && l.Id == dto.LibraryBranchId)
            .AsNoTracking().
            FirstOrDefaultAsync();

        if (library is null)
            throw new TahseenException(404, "LibraryBranch is not found");

        var updated = _mapper.Map(dto,mapped);
        updated.UpdatedAt = DateTime.UtcNow;
        var result = _repository.UpdateAsync(updated);
        return _mapper.Map<PupilBookConnectionForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
        =>await _repository.DeleteAsync(id);

    public async Task<PupilBookConnectionForResultDto> RetrieveByIdAsync(long id)
    {
        var mapped = await _repository.SelectByIdAsync(id);
        if (mapped is null || mapped.IsDeleted)
        {
            throw new TahseenException(404, "PupilBookConnection is not found");
        }

        return _mapper.Map<PupilBookConnectionForResultDto>(mapped);
    }

    public async Task<IEnumerable<PupilBookConnectionForResultDto>> RetrieveAllAsync()
    {
        var mapped = _repository.SelectAll().Where(x => !x.IsDeleted);
        return _mapper.Map<IEnumerable<PupilBookConnectionForResultDto>>(mapped);
    }
}