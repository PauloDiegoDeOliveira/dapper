using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Base;

namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Produto
{
    public class ProdutoScript : IDapperScriptBase
    {
        public string GetAll()
        {
            return @"Select distinct
                            id,
                            Nome,
                            Status,
                            CriadoEm,
                            AlteradoEm
                         From Produtos";
        }

        public string GetById()
        {
            return @"Select distinct
                            id,
                            Nome,
                            Status,
                            CriadoEm,
                            AlteradoEm
                      From Produtos
                        Where id = @id";
        }
    }
}