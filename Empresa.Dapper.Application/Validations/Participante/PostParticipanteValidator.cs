using Empresa.Dapper.Application.Dtos.Participante;
using FluentValidation;

namespace Empresa.Dapper.Application.Validations.Participante
{
    public class PostParticipanteValidator
    {
        public class PostUsuarioValidator : AbstractValidator<PostParticipanteDto>
        {
            public PostUsuarioValidator()
            {
                RuleFor(x => x.Nome)
                  .NotNull()
                  .WithMessage("O campo nome não pode ser nulo.")

                  .NotEmpty()
                  .WithMessage("O campo nome não pode ser vazio.");

                RuleFor(x => x.Sobrenome)
                  .NotNull()
                  .WithMessage("O campo sobrenome não pode ser nulo.")

                  .NotEmpty()
                  .WithMessage("O campo sobrenome não pode ser vazio.");

                RuleFor(x => x.Status)
                  .NotNull()
                  .WithMessage("O campo status não pode ser nulo.")

                  .NotEmpty()
                  .WithMessage("O campo status não pode ser vazio.")

                   .IsInEnum()
                  .WithMessage("O valor do campo status não é valido.");
            }
        }
    }
}