using Tahseen.Service.DTOs.Users.Wishlists;

namespace Tahseen.Service.Interfaces.IUsersService;

public interface IWishlistService
{
    public Task<WishlistForResultDto> AddAsync(WishlistForCreationDto dto);
    public Task<WishlistForResultDto> ModifyAsync(long id, WishlistForUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<WishlistForResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<WishlistForResultDto>> RetrieveAllAsync();
}
