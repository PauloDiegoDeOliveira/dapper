using Dapper;
using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Enums;
using Empresa.Dapper.Domain.Pagination;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.Base;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Base;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Participante;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper
{
    public class ProdutoRepository : RepositoryDapperBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IConfiguration configuration, IDapperScriptBase script) : base(configuration, script)
        {
            script = script as ParticipanteScript;
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
                    IQueryable<Produto> produtos = (await conexaoSql.QueryAsync<Produto>(new ProdutoScript().GetAll())).AsQueryable();

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