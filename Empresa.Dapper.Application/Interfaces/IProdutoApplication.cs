using Empresa.Dapper.Application.Dtos.Pagination;
using Empresa.Dapper.Application.Dtos.Produto;
using Empresa.Dapper.Application.Interfaces.Base;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Pagination;

namespace Empresa.Dapper.Application.Interfaces
{
    public interface IProdutoApplication : IApplicationBase<Produto, ViewProdutoDto, PostProdutoDto, PutProdutoDto>
    {
        Task<ViewPagedListDto<Produto, ViewProdutoDto>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave);
    }
}