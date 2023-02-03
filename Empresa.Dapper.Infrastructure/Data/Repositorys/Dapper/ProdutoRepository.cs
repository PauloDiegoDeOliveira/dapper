using Dapper;
using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Enums;
using Empresa.Dapper.Domain.Pagination;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Produto;
using Empresa.Dapper.Infrastructure.Data.Repositorys.EntityFramework.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        private readonly AppDbContext appDbContext;
        protected readonly SqlConnection conexaoSql;

        public ProdutoRepository(AppDbContext appDbContext,
                                 IConfiguration configuration) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
            conexaoSql = new SqlConnection(configuration.GetConnectionString("Connection"));
        }

        public override Task<IEnumerable<Produto>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public async Task<PagedList<Produto>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave)
        {
            try
            {
                await conexaoSql.OpenAsync();

                using (conexaoSql)
                {
                    IQueryable<Produto> produtos = (await conexaoSql.QueryAsync<Produto>(ProdutoScript.GetAll)).AsQueryable();

                    if (parametersPalavraChave.PalavraChave is null && parametersPalavraChave.Id is null && parametersPalavraChave.Status is 0)
                        produtos = produtos.Where(produto => produto.Status != EStatus.Excluido.ToString());
                    else if (parametersPalavraChave.Status != 0)
                        produtos = produtos.Where(produto => produto.Status == parametersPalavraChave.Status.ToString());

                    if (parametersPalavraChave.Id is not null)
                        produtos = produtos.Where(produto => parametersPalavraChave.Id.Contains(produto.Id));

                    // TODO: Verificar erro com like
                    if (!string.IsNullOrEmpty(parametersPalavraChave.PalavraChave))
                        produtos = produtos.Where(produto => EF.Functions.Like(produto.Nome, $"%{parametersPalavraChave.PalavraChave}%"));

                    if (parametersPalavraChave.Ordenar != 0)
                    {
                        switch (parametersPalavraChave.Ordenar.ToString())
                        {
                            case "Crescente":
                                produtos = produtos.OrderBy(produto => produto.Nome);
                                break;

                            case "Decrescente":
                                produtos = produtos.OrderByDescending(produto => produto.Nome);
                                break;

                            case "Novos":
                                produtos = produtos.OrderByDescending(produto => produto.CriadoEm);
                                break;

                            case "Antigos":
                                produtos = produtos.OrderBy(produto => produto.CriadoEm);
                                break;
                        }
                    }

                    return await Task.FromResult(PagedList<Produto>.ToPagedList(produtos, parametersPalavraChave.NumeroPagina, parametersPalavraChave.ResultadosExibidos));
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await conexaoSql.CloseAsync();
            }
        }
    }
}