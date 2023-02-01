using Empresa.Dapper.Domain.Core.Interfaces.Service.Base;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;

namespace Empresa.Dapper.Domain.Core.Interfaces.Service
{
    public interface IParticipanteService : IServiceBase<Participante>
    {
        Task<PagedList<Participante>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave);
    }
}