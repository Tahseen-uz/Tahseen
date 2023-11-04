using Tahseen.Service.DTOs.Users.Fine;

namespace Tahseen.Service.Interfaces.IUsersService
{
    public interface IFineService
    {
        public Task<FineForResultDto> AddAsync(FineForCreationDto dto);
        public Task<FineForResultDto> ModifyAsync(long Id, FineForUpdateDto dto);
        public Task<bool> RemoveAsync(long Id);
        public Task<IEnumerable<FineForResultDto>> RetrieveAllAsync();
        public Task<FineForResultDto> RetrieveByIdAsync(long Id);
    }
}
