using AutoMapper;
using Empresa.Dapper.Application.Interfaces.Base;
using Empresa.Dapper.Application.Structs;
using Empresa.Dapper.Domain.Core.Interfaces.Service.Base;
using Empresa.Dapper.Domain.Entitys.Base;
using Empresa.Dapper.Domain.Enums;

namespace Empresa.Dapper.Application.Applications.Base
{
    public class ApplicationBase<TEntity, TView, TPost, TPut> :
        IApplicationBase<TEntity, TView, TPost, TPut>
        where TEntity : EntityBase where TView : class where TPost : class where TPut : class
    {
        protected readonly IMapper mapper;
        protected readonly IServiceBase<TEntity> serviceBase;

        public ApplicationBase(IServiceBase<TEntity> serviceBase, IMapper mapper)
        {
            this.serviceBase = serviceBase;
            this.mapper = mapper;
        }

        public virtual async Task<IEnumerable<TView>> GetAllAsync()
        {
            IEnumerable<TEntity> entity = await serviceBase.GetAllAsync();
            return mapper.Map<IList<TView>>(entity);
        }

        public virtual async Task<TView> GetByIdAsync(Guid id)
        {
            TEntity entity = await serviceBase.GetByIdAsync(id);
            return mapper.Map<TView>(entity);
        }

        public virtual async Task<EntityToDto<TEntity, TPut>> MapStructById(Guid id)
        {
            TEntity entity = await serviceBase.GetByIdAsync(id);
            return new EntityToDto<TEntity, TPut>(entity, mapper.Map<TPut>(entity));
        }

        public virtual async Task<TView> PostAsync(TPost post)
        {
            TEntity entity = mapper.Map<TEntity>(post);
            entity = await serviceBase.PostAsync(entity);
            return mapper.Map<TView>(entity);
        }

        public virtual async Task<TView> PutAsync(TPut put)
        {
            TEntity entity = mapper.Map<TEntity>(put);
            entity = await serviceBase.PutAsync(entity);
            return mapper.Map<TView>(entity);
        }

        public virtual async Task<TView> DeleteAsync(Guid id)
        {
            TEntity entity = await serviceBase.DeleteAsync(id);
            return mapper.Map<TView>(entity);
        }

        public virtual async Task<TView> PutStatusAsync(Guid id, EStatus status)
        {
            TEntity queryEntity = await serviceBase.GetByIdAsync(id);

            if (queryEntity is null)
                return null;

            queryEntity.ChangeStatusValue(status.ToString());

            TEntity entity = await serviceBase.PutStatusAsync(queryEntity);
            return mapper.Map<TView>(entity);
        }

        public virtual async Task<bool> ExisteNaBaseAsync(Guid id)
        {
            return await serviceBase.ExisteNaBaseAsync(id);
        }

        public virtual async Task MapStructSaveChangesAsync(EntityToDto<TEntity, TPut> dtoStruct)
        {
            TEntity entity = mapper.Map(dtoStruct.Dto, dtoStruct.Entity);
            await serviceBase.SaveChangesAsync(entity);
        }
    }
}