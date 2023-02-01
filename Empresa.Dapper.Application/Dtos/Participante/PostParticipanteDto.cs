using Empresa.Dapper.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Empresa.Dapper.Application.Dtos.Participante
{
    public class PostParticipanteDto
    {
        /// <summary>
        /// Nome
        /// </summary>
        /// <example>Paulo</example>
        [Display(Name = "nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        /// <example>Diego</example>
        [Display(Name = "sobrenome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Sobrenome { get; set; }

        public string CPF { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [Display(Name = "status")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EnumDataType(typeof(EStatus), ErrorMessage = "O campo {0} está em formato inválido.")]
        public EStatus Status { get; set; }
    }
}