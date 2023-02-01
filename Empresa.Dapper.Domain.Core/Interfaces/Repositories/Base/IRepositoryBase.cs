using Empresa.Dapper.Domain.Entitys.Base;

namespace Empresa.Dapper.Domain.Core.Interfaces.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdNoTrackingAsync(Guid id);

        Task<TEntity> GetByIdTrackingAsync(Guid id);

        Task<TEntity> PostAsync(TEntity entity);

        Task<TEntity> PutAsync(TEntity entity);

        Task<TEntity> PutStatusAsync(TEntity entity);

        Task<TEntity> DeleteAsync(Guid id);

        Task<bool> ExisteNaBaseAsync(Guid id);

        Task SaveChangesAsync(TEntity entity);
    }
}