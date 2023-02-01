using Empresa.Dapper.Domain.Core.Interfaces.Repositories.Base;
using Empresa.Dapper.Domain.Entitys.Base;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private readonly AppDbContext appDbContext;

        public RepositoryBase(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await appDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdNoTrackingAsync(Guid id)
        {
            return await appDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetByIdTrackingAsync(Guid id)
        {
            return await appDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> PostAsync(TEntity entity)
        {
            appDbContext.Set<TEntity>().Add(entity);
            await appDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> PutAsync(TEntity entity)
        {
            var queryEntity = await GetByIdNoTrackingAsync(entity.Id);

            if (queryEntity is null)
                return null;

            appDbContext.Entry(entity).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> PutStatusAsync(TEntity entity)
        {
            appDbContext.Entry(entity).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(Guid id)
        {
            var queryEntity = await GetByIdNoTrackingAsync(id);

            if (queryEntity is not null)
            {
                appDbContext.Remove(queryEntity);
                await appDbContext.SaveChangesAsync();
            }

            return queryEntity;
        }

        public virtual async Task<bool> ExisteNaBaseAsync(Guid id)
        {
            return await appDbContext.Set<TEntity>().AnyAsync(x => x.Id == id);
        }

        public virtual async Task SaveChangesAsync(TEntity entity)
        {
            appDbContext.Entry(entity).State = EntityState.Modified;
            await appDbContext.SaveChangesAsync();
        }
    }
}