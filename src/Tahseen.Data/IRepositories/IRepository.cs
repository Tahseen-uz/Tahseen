namespace Tahseen.Data.IRepositories;

public interface IRepository<TEntity>
{
    public Task<TEntity> CreateAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<bool> DeleteAsync(long Id);
    public Task<TEntity> SelectByIdAsync(long Id);
    public IQueryable<TEntity> SelectAll();

}

