using Tahseen.Data.IRepositories;
using Tahseen.Domain.Commons;

namespace Tahseen.Service.Services.Book;

public class BaseService<T> where T : Auditable
{
    private IRepostory<T> Repostory { get; set; }

    public BaseService(IRepostory<T> repostory)
    {
        Repostory = repostory;
    }

    public async Task<T> CreateAsync(T data)
    {
        return await Repostory.CreateAsync(data);
    }

    public async Task<T> UpdateAsync(T data)
    {
        if (!data.IsDeleted)
        {
            return await Repostory.UpdateAsync(data);
        }

        throw new Exception("Data already deleted");
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var data =await GetByIdAsync(id);
        if (!data.IsDeleted)
        {
            return await Repostory.DeleteAsync(id);
        }
        throw new Exception("Data already deleted");
    
    }
    
    public async Task<T> GetByIdAsync(long id)
    {
        return await Repostory.SelectByIdAsync(id);
    }
    
    public async Task<List<T>> SelectAllAsync()
    {
        return  Repostory.SelectAll().ToList();
    }
    
    public async Task<List<T>> SelectAllIsDeletedAsync()
    {
        return  Repostory.SelectAll().Where(data=>data.IsDeleted==true).ToList();
    }
}
    