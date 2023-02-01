using Empresa.Dapper.Domain.Entitys.Base;

namespace Empresa.Dapper.Domain.Core.Interfaces.Service.Base
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Guid id);

        Task<TEntity> PostAsync(TEntity entity);

        Task<TEntity> PutAsync(TEntity entity);

        Task<TEntity> PutStatusAsync(TEntity entity);

        Task<TEntity> DeleteAsync(Guid id);

        Task<bool> ExisteNaBaseAsync(Guid id);

        Task SaveChangesAsync(TEntity entity);
    }
}