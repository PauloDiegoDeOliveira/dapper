using Dapper;
using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Base;
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
    }
}