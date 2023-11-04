using Tahseen.Service.DTOs.SchoolAndEducations;

namespace Tahseen.Service.Interfaces.ISchoolAndEducation;

public interface ISchoolBookService
{
    public Task<SchoolBookForResultDto> AddAsync(SchoolBookForCreationDto dto);
    public Task<SchoolBookForResultDto> ModifyAsync(long id, SchoolBookForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<SchoolBookForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<SchoolBookForResultDto>> RetrieveAllAsync();
}