using Tahseen.Service.DTOs.AudioBooks.AudioFile;

namespace Tahseen.Service.Interfaces.IAudioBookServices;

public interface IAudioFileService
{
    public Task<AudioFileForResultDto> AddAsync(AudioFileForCreationDto dto);
    public Task<AudioFileForResultDto> ModifyAsync(long id, AudioFileForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<AudioFileForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<AudioFileForResultDto>> RetrieveAllAsync();
}
