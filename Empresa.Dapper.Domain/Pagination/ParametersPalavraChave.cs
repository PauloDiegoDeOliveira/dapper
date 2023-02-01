using Empresa.Dapper.Domain.Enums;
using Empresa.Dapper.Domain.Pagination.Base;

namespace Empresa.Dapper.Domain.Pagination
{
    public class ParametersPalavraChave : ParametersBase
    {
        public string PalavraChave { get; set; }

        public EOrdenar Ordenar { get; set; }
    }
}