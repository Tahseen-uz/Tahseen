using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities.SchoolAndEducations;
using Tahseen.Service.DTOs.SchoolAndEducations;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.ISchoolAndEducation;

namespace Tahseen.Service.Services.SchoolAndEducations;

public class SchoolBookService:ISchoolBookService
{
    private readonly IMapper _mapper;
    private readonly IRepository<SchoolBook> _repository;

    public SchoolBookService(IMapper mapper, IRepository<SchoolBook>repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<SchoolBookForResultDto> AddAsync(SchoolBookForCreationDto dto)
    {
        var Data = this._repository.SelectAll().Where(s => s.LibraryBranchId == dto.LibraryBranchId && s.SchoolBookTitle == dto.SchoolBookTitle && s.Grade == dto.Grade && s.IsDeleted == false);
        if(Data != null)
        {
            throw new TahseenException(409, "This book is exist");
        }
        var mapped = _mapper.Map<SchoolBook>(dto);
        var result = _repository.CreateAsync(mapped);
        return _mapper.Map<SchoolBookForResultDto>(result);
    }

    public async Task<SchoolBookForResultDto> ModifyAsync(long id, SchoolBookForUpdateDto dto)
    {
        var book = await _repository.SelectAll().Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefaultAsync();
        if (book is null)
        {
            throw new TahseenException(404, "School Book is not found");
        }
        var mapped = _mapper.Map(dto, book);
        mapped.UpdatedAt = DateTime.UtcNow;
        var result = _repository.UpdateAsync(mapped);
        return _mapper.Map<SchoolBookForResultDto>(result);
    }

    public async Task<bool> RemoveAsync(long id)
        => await _repository.DeleteAsync(id);

    public async Task<SchoolBookForResultDto> RetrieveByIdAsync(long id)
    {
        var book =await _repository.SelectByIdAsync(id);
        if (book is null ||book.IsDeleted)
        {
            throw new TahseenException(404, "SchoolBooks not found");
        }
        return _mapper.Map<SchoolBookForResultDto>(book);
    }

    public async Task<IEnumerable<SchoolBookForResultDto>> RetrieveAllAsync()
    {
        var books = _repository.SelectAll().Where(x => !x.IsDeleted);
        return _mapper.Map<IEnumerable<SchoolBookForResultDto>>(books);
    }
}