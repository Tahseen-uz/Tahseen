using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.Events.Events;
using Tahseen.Service.DTOs.Languages;

namespace Tahseen.Service.Interfaces.ILanguageServices;

public interface ILanguageService
{
    public Task<LanguageForResultDto> AddAsync(LanguageForCreationDto dto);
    public Task<LanguageForResultDto> ModifyAsync(long Id, LanguageForUpdateDto dto);
    public Task<bool> RemoveAsync(long Id);
    public Task<LanguageForResultDto> RetrieveByIdAsync(long Id);
    public Task<IEnumerable<LanguageForResultDto>> RetrieveAllAsync();
}
