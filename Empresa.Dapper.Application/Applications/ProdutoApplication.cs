using AutoMapper;
using Empresa.Dapper.Application.Applications.Base;
using Empresa.Dapper.Application.Dtos.Pagination;
using Empresa.Dapper.Application.Dtos.Produto;
using Empresa.Dapper.Application.Interfaces;
using Empresa.Dapper.Domain.Core.Interfaces.Service;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;

namespace Empresa.Dapper.Application.Applications
{
    public class ProdutoApplication : ApplicationBase<Produto, ViewProdutoDto, PostProdutoDto, PutProdutoDto>, IProdutoApplication
    {
        private readonly IProdutoService produtoService;

        public ProdutoApplication(IProdutoService produtoService,
                                  IMapper mapper) : base(produtoService, mapper)
        {
            this.produtoService = produtoService;
        }

        public async Task<ViewPagedListDto<Produto, ViewProdutoDto>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave)
        {
            PagedList<Produto> pagedList = await produtoService.GetPaginationAsync(parametersPalavraChave);
            return new ViewPagedListDto<Produto, ViewProdutoDto>(pagedList, mapper.Map<List<ViewProdutoDto>>(pagedList));
        }
    }
}