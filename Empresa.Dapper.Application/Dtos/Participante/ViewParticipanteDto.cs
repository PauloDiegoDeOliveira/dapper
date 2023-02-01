using Empresa.Dapper.Domain.Enums;

namespace Empresa.Dapper.Application.Dtos.Participante
{
    public class ViewParticipanteDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPF { get; set; }

        public EStatus Status { get; set; }
    }
}