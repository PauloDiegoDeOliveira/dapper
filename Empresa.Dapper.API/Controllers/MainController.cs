using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Core.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Empresa.Dapper.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador notificador;
        public readonly IUser AppUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }
        protected IEnumerable<string> UsuarioClaims { get; set; }

        protected MainController(INotificador notificador,
                                 IUser appUser)
        {
            this.notificador = notificador;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
                UsuarioClaims = appUser.GetUserClaims();
            }
        }

        protected bool OperacaoValida()
        {
            return !notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null, string mensagem = "")
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    mensagem,
                    sucesso = true,
                    dados = result
                });
            }

            return BadRequest(new
            {
                sucesso = false,
                erros = notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotificarErroModelInvalida(modelState);

            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                string errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            notificador.Handle(new Notificacao(mensagem));
        }

        protected void NotificarErro(List<string> mensagemList)
        {
            foreach (string erro in mensagemList)
            {
                NotificarErro(erro);
            }
        }
    }
}