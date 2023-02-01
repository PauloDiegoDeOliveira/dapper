using Empresa.Dapper.Domain.Entitys.Base;

namespace Empresa.Dapper.Domain.Entitys
{
    public class Participante : EntityBase
    {
        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string CPF { get; set; }
    }
}