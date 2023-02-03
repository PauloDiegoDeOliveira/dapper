namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Produto
{
    public class ProdutoScript
    {
        public const string GetAll = @"Select distinct
                                              id,
                                              Nome,
                                              Status,
                                              CriadoEm,
                                              AlteradoEm
                                          From Produtos";

        public const string GetById = @"Select distinct
                                                id,
                                                Nome,
                                                Status,
                                                CriadoEm,
                                                AlteradoEm
                                          From Produtos
                                            Where id = @id";
    }
}