using Empresa.Dapper.Domain.Core.Notificacoes;

namespace Empresa.Dapper.Domain.Core.Interfaces.Service
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);
    }
}