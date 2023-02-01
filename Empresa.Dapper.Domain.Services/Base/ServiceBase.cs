using Empresa.Dapper.Domain.Core.Interfaces.Repositories.Base;
using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Core.Interfaces.Service.Base;
using Empresa.Dapper.Domain.Core.Notificacoes;
using Empresa.Dapper.Domain.Entitys.Base;

namespace Empresa.Dapper.Domain.Services.Base
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        private readonly IRepositoryBase<TEntity> repositoryBase;
        private readonly INotificador notificador;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase,
                           INotificador notificador)
        {
            this.repositoryBase = repositoryBase;
            this.notificador = notificador;
        }

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        }

        protected void Notificar(string mensagem)
        {
            notificador.Handle(new Notificacao(mensagem));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await repositoryBase.GetAllAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await repositoryBase.GetByIdNoTrackingAsync(id);
        }

        public virtual async Task<TEntity> PostAsync(TEntity entity)
        {
            return await repositoryBase.PostAsync(entity);
        }

        public virtual async Task<TEntity> PutAsync(TEntity entity)
        {
            return await repositoryBase.PutAsync(entity);
        }

        public virtual async Task<TEntity> PutStatusAsync(TEntity entity)
        {
            return await repositoryBase.PutStatusAsync(entity);
        }

        public virtual async Task<TEntity> DeleteAsync(Guid id)
        {
            return await repositoryBase.DeleteAsync(id);
        }

        public virtual async Task<bool> ExisteNaBaseAsync(Guid id)
        {
            return await repositoryBase.ExisteNaBaseAsync(id);
        }

        public virtual async Task SaveChangesAsync(TEntity entity)
        {
            await repositoryBase.SaveChangesAsync(entity);
        }
    }
}