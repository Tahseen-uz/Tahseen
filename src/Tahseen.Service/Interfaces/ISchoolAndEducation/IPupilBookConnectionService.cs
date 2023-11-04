using Tahseen.Service.DTOs.Books.Author;
using Tahseen.Service.DTOs.SchoolAndEducations;

namespace Tahseen.Service.Interfaces.ISchoolAndEducation;

public interface IPupilBookConnectionService
{
    public Task<PupilBookConnectionForResultDto> AddAsync(PupilBookConnectionForCreationDto dto);
    public Task<PupilBookConnectionForResultDto> ModifyAsync(long id, PupilBookConnectionForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<PupilBookConnectionForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<PupilBookConnectionForResultDto>> RetrieveAllAsync();
}