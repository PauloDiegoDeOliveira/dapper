//using Dapper;
//using Empresa.Dapper.Domain.Core.Interfaces.Repositories.Base;
//using Empresa.Dapper.Domain.Entitys.Base;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;

//namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.Base
//{
//    public class RepositoryDapperBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
//    {
//        protected readonly SqlConnection conexaoSql;

//        public RepositoryDapperBase(IConfiguration configuration)
//        {
//            conexaoSql = new SqlConnection(configuration.GetConnectionString("Connection"));
//        }

//        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
//        {
//            IQueryable<TEntity> entidade = (await conexaoSql.QueryAsync<TEntity>(script.GetAll())).AsQueryable();
//            return entidade;
//        }

//        public Task<TEntity> DeleteAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> ExisteNaBaseAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TEntity> GetByIdNoTrackingAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TEntity> GetByIdTrackingAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TEntity> PostAsync(TEntity entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TEntity> PutAsync(TEntity entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TEntity> PutStatusAsync(TEntity entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task SaveChangesAsync(TEntity entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}