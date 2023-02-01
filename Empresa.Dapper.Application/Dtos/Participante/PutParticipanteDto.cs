using System.ComponentModel.DataAnnotations;

namespace Empresa.Dapper.Application.Dtos.Participante
{
    public class PutParticipanteDto : PostParticipanteDto
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <example>085acbb3-a6b5-4cfa-dc22-08daa7d24f76</example>
        [Display(Name = "id")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid Id { get; set; }
    }
}