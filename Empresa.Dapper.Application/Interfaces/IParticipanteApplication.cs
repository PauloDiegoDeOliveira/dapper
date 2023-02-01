using Empresa.Dapper.Application.Dtos.Pagination;
using Empresa.Dapper.Application.Interfaces.Base;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;

namespace Empresa.Dapper.Application.Interfaces
{
    public interface IParticipanteApplication : IApplicationBase<Participante, ViewParticipanteDto, PostParticipanteDto, PutParticipanteDto>
    {
        Task<ViewPagedListDto<Participante, ViewParticipanteDto>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave);
    }
}