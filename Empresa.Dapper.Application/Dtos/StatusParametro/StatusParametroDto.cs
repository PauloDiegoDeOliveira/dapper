using Empresa.Dapper.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Empresa.Dapper.Application.Dtos.StatusParametro
{
    /// <summary>
    /// Objeto utilizado para inserção.
    /// </summary>
    public class StatusParametroDto
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <example>3DF086FC-B01F-4918-F7C5-08DA90D8A47C</example>
        [Display(Name = "id")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid Id { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        /// <example>Ativo</example>
        [Display(Name = "status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EnumDataType(typeof(EStatus), ErrorMessage = "O campo {0} está em formato inválido.")]
        public EStatus Status { get; set; }
    }
}