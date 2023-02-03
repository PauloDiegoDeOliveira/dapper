using Empresa.Dapper.Domain.Enums;

namespace Empresa.Dapper.Application.Dtos.Produto
{
    public class ViewProdutoDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public EStatus Status { get; set; }
    }
}