using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;
using Empresa.Dapper.Domain.Services.Base;

namespace Empresa.Dapper.Domain.Services
{
    public class ProdutoService : ServiceBase<Produto>, IProdutoService
    {
        private readonly IProdutoRepository produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository) : base(produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        public async Task<PagedList<Produto>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave)
        {
            return await produtoRepository.GetPaginationAsync(parametersPalavraChave);
        }
    }
}