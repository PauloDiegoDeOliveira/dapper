using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;
using Empresa.Dapper.Domain.Services.Base;

namespace Empresa.Dapper.Domain.Services
{
    public class ParticipanteService : ServiceBase<Participante>, IParticipanteService
    {
        private readonly IParticipanteRepository participanteRepository;

        public ParticipanteService(IParticipanteRepository participanteRepository) : base(participanteRepository)
        {
            this.participanteRepository = participanteRepository;
        }

        public async Task<PagedList<Participante>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave)
        {
            return await participanteRepository.GetPaginationAsync(parametersPalavraChave);
        }
    }
}