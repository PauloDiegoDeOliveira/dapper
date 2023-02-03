using Empresa.Dapper.API.Controllers;
using Empresa.Dapper.Application.Dtos.Pagination;
using Empresa.Dapper.Application.Dtos.Participante;
using Empresa.Dapper.Application.Dtos.StatusParametro;
using Empresa.Dapper.Application.Interfaces;
using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Enums;
using Empresa.Dapper.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using SerilogTimings;

namespace Empresa.Dapper.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/participantes")]
    [ApiController]
    public class ParticipanteController : MainController
    {
        private readonly IParticipanteApplication participanteApplication;
        private readonly ILogger<ParticipanteController> logger;

        public ParticipanteController(IParticipanteApplication participanteApplication,
                                      INotificador notificador,
                                      ILogger<ParticipanteController> logger,
                                      IUser appUser) : base(notificador, appUser)
        {
            this.participanteApplication = participanteApplication;
            this.logger = logger;
        }


        /// <summary>
        /// Retorna todos os participantes com filtro e paginação de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet("teste")]
        [ProducesResponseType(typeof(ViewPagedListDto<Participante, ViewParticipanteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            logger.LogWarning("Foi requisitado os participantes.");

            IEnumerable<ViewParticipanteDto> cargos = await participanteApplication.GetAllAsync();

            if (cargos is null)
            {
                NotificarErro("Nenhum participante foi encontrado.");
                return CustomResponse(ModelState);
            }

            return CustomResponse(cargos, "Participantes encontrados.");
        }

        /// <summary>
        /// Retorna todos os participantes com filtro e paginação de dados.
        /// </summary>
        /// <param name="parametersPalavraChave"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ViewPagedListDto<Participante, ViewParticipanteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] ParametersPalavraChave parametersPalavraChave)
        {
            logger.LogWarning("Foi requisitado os participantes.");

            ViewPagedListDto<Participante, ViewParticipanteDto> cargos = await participanteApplication.GetPaginationAsync(parametersPalavraChave);

            if (cargos.Pagina.Count is 0)
            {
                NotificarErro("Nenhum participante foi encontrado.");
                return CustomResponse(ModelState);
            }

            return CustomResponse(cargos, "Participantes encontrados.");
        }

        /// <summary>
        /// Insere um novo participante.
        /// </summary>
        /// <param name="postParticipanteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ViewParticipanteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] PostParticipanteDto postParticipanteDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            logger.LogWarning("Objeto recebido {@postParticipanteDto}", postParticipanteDto);

            ViewParticipanteDto inserido;
            using (Operation.Time("Tempo de adição de um participante."))
            {
                logger.LogWarning("Foi requisitada a inserção de um participante.");
                inserido = await participanteApplication.PostAsync(postParticipanteDto);
            }

            return CustomResponse(inserido, "Participante criado com sucesso!");
        }

        /// <summary>
        /// Altera um participante.
        /// </summary>
        /// <param name="putCargoDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ViewParticipanteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] PutParticipanteDto putParticipanteDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            logger.LogWarning("Objeto recebido {@putParticipanteDto}", putParticipanteDto);

            ViewParticipanteDto atualizado;
            using (Operation.Time("Tempo de atualização de um participante."))
            {
                logger.LogWarning("Foi requisitada a atualização de um participante.");
                atualizado = await participanteApplication.PutAsync(putParticipanteDto);
            }

            if (atualizado is null)
            {
                NotificarErro("Nenhum participante foi encontrado com o id informado.");
                return CustomResponse(ModelState);
            }

            return CustomResponse(atualizado, "Participante atualizado com sucesso!");
        }

        /// <summary>
        /// Exclui um participante.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Ao excluir um participante o mesmo será alterado para status 3 excluído.</remarks>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ViewParticipanteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            ViewParticipanteDto removido = await participanteApplication.PutStatusAsync(id, EStatus.Excluido);

            if (removido is null)
            {
                NotificarErro("Nenhum participante foi encontrado com o id informado.");
                return CustomResponse(ModelState);
            }

            logger.LogWarning("Objeto removido {@removido} ", removido);

            return CustomResponse(removido, "Participante excluído com sucesso.");
        }

        /// <summary>
        /// Altera o status.
        /// </summary>
        /// <param name="statusParametroDto"></param>
        /// <returns></returns>
        [HttpPut("status")]
        [ProducesResponseType(typeof(ViewParticipanteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStatusAsync([FromBody] StatusParametroDto statusParametroDto)
        {
            if (statusParametroDto.Status == 0)
            {
                NotificarErro("Nenhum status selecionado!");
                return CustomResponse(ModelState);
            }

            logger.LogWarning("Objeto recebido {@statusParametroDto}", statusParametroDto);

            ViewParticipanteDto atualizado;
            using (Operation.Time("Tempo de atualização do status de um participante."))
            {
                logger.LogWarning("Foi requisitada a atualização do status de um participante.");
                atualizado = await participanteApplication.PutStatusAsync(statusParametroDto.Id, statusParametroDto.Status);
            }

            if (atualizado is null)
            {
                NotificarErro("Nenhum participante foi encontrado com o id informado.");
                return CustomResponse(ModelState);
            }

            return atualizado.Status switch
            {
                EStatus.Ativo => CustomResponse(atualizado, "Participante atualizado para ativo com sucesso."),
                EStatus.Inativo => CustomResponse(atualizado, "Participante atualizado para inativo com sucesso."),
                EStatus.Excluido => CustomResponse(atualizado, "Participante atualizado para excluído com sucesso."),
                _ => CustomResponse(atualizado, "Status atualizado com sucesso."),
            };
        }
    }
}