using Empresa.Dapper.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Empresa.Dapper.Application.Dtos.Produto
{
    public class PostProdutoDto
    {
        /// <summary>
        /// Nome
        /// </summary>
        /// <example>Teclado</example>
        [Display(Name = "nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EnumDataType(typeof(EStatus), ErrorMessage = "O campo {0} está em formato inválido.")]
        public EStatus Status { get; set; }
    }
}