using Empresa.Dapper.Domain.Entitys.Base;

namespace Empresa.Dapper.Domain.Entitys
{
    public class Produto : EntityBase
    {
        public string Nome { get; private set; }
    }
}