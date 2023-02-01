using Dapper;
using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Base;
using Empresa.Dapper.Infrastructure.Data.Repositorys.ScriptsSql;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Empresa.Dapper.Infrastructure.Data.Repositorys
{
    public class ParticipanteRepository : RepositoryBase<Participante>, IParticipanteRepository
    {
        private readonly AppDbContext appDbContext;
        private IConfiguration configuration;

        public ParticipanteRepository(AppDbContext appDbContext,
                                      IConfiguration configuration) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
            this.configuration = configuration;
        }

        public string GetConnection()
        {
            string connection = configuration.GetSection("ConnectionStrings").GetSection("Connection").Value;
            return connection;
        }

        public async Task<PagedList<Participante>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave)
        {
            string connectionString = this.GetConnection();

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                string query = ParticipanteScript.GetAll;
                List<Participante> participantes = (await conexao.QueryAsync<Participante>(sql: query)).ToList();

                return await Task.FromResult(PagedList<Participante>.ToPagedList(participantes.AsQueryable(), parametersPalavraChave.NumeroPagina, parametersPalavraChave.ResultadosExibidos));
            }
        }
    }
}