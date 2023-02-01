using Empresa.Dapper.Domain.Core.Interfaces.Repositories.Base;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;

namespace Empresa.Dapper.Domain.Core.Interfaces.Repositories
{
    public interface IParticipanteRepository : IRepositoryBase<Participante>
    {
        Task<PagedList<Participante>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave);
    }
}