using Tahseen.Service.DTOs.SchoolAndEducations;

namespace Tahseen.Service.Interfaces.ISchoolAndEducation;

public interface IPupilService
{
    public Task<PupilForResultDto> AddAsync(PupilForCreationDto dto);
    public Task<PupilForResultDto> ModifyAsync(long id, PupilForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<PupilForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<PupilForResultDto>> RetrieveAllAsync();
}