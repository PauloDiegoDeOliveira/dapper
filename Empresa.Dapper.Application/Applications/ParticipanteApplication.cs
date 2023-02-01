using AutoMapper;
using Empresa.Dapper.Application.Applications.Base;
using Empresa.Dapper.Application.Dtos.Pagination;
using Empresa.Dapper.Application.Interfaces;
using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;

namespace Empresa.Dapper.Application.Applications
{
    public class ParticipanteApplication : ApplicationBase<Participante, ViewParticipanteDto, PostParticipanteDto, PutParticipanteDto>, IParticipanteApplication
    {
        private readonly IParticipanteService participanteService;

        public ParticipanteApplication(IParticipanteService participanteService,
                                       IMapper mapper) : base(participanteService, mapper)
        {
            this.participanteService = participanteService;
        }

        public async Task<ViewPagedListDto<Participante, ViewParticipanteDto>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave)
        {
            PagedList<Participante> pagedList = await cargoService.GetPaginationAsync(parametersPalavraChave);
            return new ViewPagedListDto<Participante, ViewParticipanteDto>(pagedList, mapper.Map<List<ViewParticipanteDto>>(pagedList));
        }
    }
}