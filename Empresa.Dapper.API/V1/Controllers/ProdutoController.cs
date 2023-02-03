using Empresa.Dapper.API.Controllers;
using Empresa.Dapper.Application.Dtos.Pagination;
using Empresa.Dapper.Application.Dtos.Produto;
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
    [Route("/v{version:apiVersion}/produtos")]
    [ApiController]
    public class ProdutoController : MainController
    {
        private readonly IProdutoApplication produtoApplication;
        private readonly ILogger<ProdutoController> logger;

        public ProdutoController(IProdutoApplication produtoApplication,
                                 INotificador notificador,
                                 ILogger<ProdutoController> logger,
                                 IUser appUser) : base(notificador, appUser)
        {
            this.produtoApplication = produtoApplication;
            this.logger = logger;
        }

        /// <summary>
        /// Retorna todos os produtos com filtro e paginação de dados.
        /// </summary>
        /// <returns></returns>
        [HttpGet("teste")]
        [ProducesResponseType(typeof(ViewPagedListDto<Produto, ViewProdutoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            logger.LogWarning("Foi requisitado os produtos.");

            IEnumerable<ViewProdutoDto> cargos = await produtoApplication.GetAllAsync();

            if (cargos is null)
            {
                NotificarErro("Nenhum produto foi encontrado.");
                return CustomResponse(ModelState);
            }

            return CustomResponse(cargos, "Produtos encontrados.");
        }

        /// <summary>
        /// Retorna todos os produtos com filtro e paginação de dados.
        /// </summary>
        /// <param name="parametersPalavraChave"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ViewPagedListDto<Produto, ViewProdutoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] ParametersPalavraChave parametersPalavraChave)
        {
            logger.LogWarning("Foi requisitado os produtos.");

            ViewPagedListDto<Produto, ViewProdutoDto> cargos = await produtoApplication.GetPaginationAsync(parametersPalavraChave);

            if (cargos.Pagina.Count is 0)
            {
                NotificarErro("Nenhum produto foi encontrado.");
                return CustomResponse(ModelState);
            }

            return CustomResponse(cargos, "Produtos encontrados.");
        }

        /// <summary>
        /// Insere um novo produto.
        /// </summary>
        /// <param name="postParticipanteDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ViewProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] PostProdutoDto postProdutoDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            logger.LogWarning("Objeto recebido {@postProdutoDto}", postProdutoDto);

            ViewProdutoDto inserido;
            using (Operation.Time("Tempo de adição de um produto."))
            {
                logger.LogWarning("Foi requisitada a inserção de um produto.");
                inserido = await produtoApplication.PostAsync(postProdutoDto);
            }

            return CustomResponse(inserido, "Produto criado com sucesso!");
        }

        /// <summary>
        /// Altera um produto.
        /// </summary>
        /// <param name="putCargoDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(ViewProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync([FromBody] PutProdutoDto putProdutoDto)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            logger.LogWarning("Objeto recebido {@putProdutoDto}", putProdutoDto);

            ViewProdutoDto atualizado;
            using (Operation.Time("Tempo de atualização de um produto."))
            {
                logger.LogWarning("Foi requisitada a atualização de um produto.");
                atualizado = await produtoApplication.PutAsync(putProdutoDto);
            }

            if (atualizado is null)
            {
                NotificarErro("Nenhum produto foi encontrado com o id informado.");
                return CustomResponse(ModelState);
            }

            return CustomResponse(atualizado, "Produto atualizado com sucesso!");
        }

        /// <summary>
        /// Exclui um produto.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Ao excluir um produto o mesmo será alterado para status 3 excluído.</remarks>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ViewProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            ViewProdutoDto removido = await produtoApplication.PutStatusAsync(id, EStatus.Excluido);

            if (removido is null)
            {
                NotificarErro("Nenhum produto foi encontrado com o id informado.");
                return CustomResponse(ModelState);
            }

            logger.LogWarning("Objeto removido {@removido} ", removido);

            return CustomResponse(removido, "Produto excluído com sucesso.");
        }

        /// <summary>
        /// Altera o status.
        /// </summary>
        /// <param name="statusParametroDto"></param>
        /// <returns></returns>
        [HttpPut("status")]
        [ProducesResponseType(typeof(ViewProdutoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutStatusAsync([FromBody] StatusParametroDto statusParametroDto)
        {
            if (statusParametroDto.Status == 0)
            {
                NotificarErro("Nenhum status selecionado!");
                return CustomResponse(ModelState);
            }

            logger.LogWarning("Objeto recebido {@statusParametroDto}", statusParametroDto);

            ViewProdutoDto atualizado;
            using (Operation.Time("Tempo de atualização do status de um produto."))
            {
                logger.LogWarning("Foi requisitada a atualização do status de um produto.");
                atualizado = await produtoApplication.PutStatusAsync(statusParametroDto.Id, statusParametroDto.Status);
            }

            if (atualizado is null)
            {
                NotificarErro("Nenhum participante foi encontrado com o id informado.");
                return CustomResponse(ModelState);
            }

            return atualizado.Status switch
            {
                EStatus.Ativo => CustomResponse(atualizado, "Produto atualizado para ativo com sucesso."),
                EStatus.Inativo => CustomResponse(atualizado, "Produto atualizado para inativo com sucesso."),
                EStatus.Excluido => CustomResponse(atualizado, "Produto atualizado para excluído com sucesso."),
                _ => CustomResponse(atualizado, "Status atualizado com sucesso."),
            };
        }
    }
}