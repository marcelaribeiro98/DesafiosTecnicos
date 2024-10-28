using FIAP.GestaoEscolar.Domain.Requests.StudentClass;
using FluentValidation;

namespace FIAP.GestaoEscolar.Application.Validators.StudentClass
{
    public class UpdateStudentClassValidator : AbstractValidator<UpdateStudentClassRequest>
    {
        public UpdateStudentClassValidator()
        {
            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("O ID do aluno é obrigatório.");

            RuleFor(x => x.ClassId)
                .NotNull().WithMessage("O novo ID da turma é obrigatório.");

            RuleFor(x => x.CurrentClassId)
                .NotNull().WithMessage("O atual ID da turma é obrigatório.");

            RuleFor(x => x.Active)
                .NotNull().WithMessage("O ativo é obrigatório.");
        }

    }
}