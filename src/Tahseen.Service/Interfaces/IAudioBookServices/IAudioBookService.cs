using Tahseen.Service.Configurations;
using Tahseen.Service.DTOs.AudioBooks.AudioBook;
using Tahseen.Service.DTOs.EBooks.EBook;

namespace Tahseen.Service.Interfaces.IAudioBookServices;

public interface IAudioBookService
{
    public Task<AudioBookForResultDto> AddAsync(AudioBookForCreationDto dto);
    public Task<AudioBookForResultDto> ModifyAsync(long id, AudioBookForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<AudioBookForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<AudioBookForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
