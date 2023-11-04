using Microsoft.EntityFrameworkCore;
using Tahseen.Data.DbContexts;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Commons;

namespace Tahseen.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        AppDbContext dbContext;
        DbSet<TEntity> dbSet;
        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext; 
            this.dbSet = dbContext.Set<TEntity>();  
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            var result = await dbSet.Where(e => e.Id == Id && !e.IsDeleted).FirstOrDefaultAsync();
            result.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public IQueryable<TEntity> SelectAll()
         => this.dbSet;
        public async Task<TEntity> SelectByIdAsync(long Id)
        {
            var result = await this.dbSet.Where(e => e.Id == Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = (dbContext.Update(entity)).Entity;
            await dbContext.SaveChangesAsync();
            return result;
        }

    }
}
