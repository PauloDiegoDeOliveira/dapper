using Empresa.Dapper.Application.Structs;
using Empresa.Dapper.Domain.Entitys.Base;
using Empresa.Dapper.Domain.Enums;

namespace Empresa.Dapper.Application.Interfaces.Base
{
    public interface IApplicationBase<TEntity, TView, TPost, TPut>
            where TView : class where TPost : class where TPut : class where TEntity : EntityBase
    {
        Task<IEnumerable<TView>> GetAllAsync();

        Task<TView> GetByIdAsync(Guid id);

        Task<EntityToDto<TEntity, TPut>> MapStructById(Guid id);

        Task<TView> PostAsync(TPost post);

        Task<TView> PutAsync(TPut put);

        Task<TView> PutStatusAsync(Guid id, EStatus status);

        Task<TView> DeleteAsync(Guid id);

        Task<bool> ExisteNaBaseAsync(Guid id);

        Task MapStructSaveChangesAsync(EntityToDto<TEntity, TPut> dtoStruct);
    }
}