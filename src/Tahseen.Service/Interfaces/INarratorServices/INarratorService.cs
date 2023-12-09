using Tahseen.Service.DTOs.Narrators;

namespace Tahseen.Service.Interfaces.INurratorServices;

public interface INarratorService
{
    public Task<NarratorForResultDto> AddAsync(NarratorForCreationDto dto);
    public Task<NarratorForResultDto> ModifyAsync(long id, NarratorForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<NarratorForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<NarratorForResultDto>> RetrieveAllAsync();
}
