using FIAP.GestaoEscolar.Domain.Requests.StudentClass;
using FluentValidation;

namespace FIAP.GestaoEscolar.Application.Validators.StudentClass
{
    public class CreateStudentClassValidator : AbstractValidator<CreateStudentClassRequest>
    {
        public CreateStudentClassValidator()
        {
            RuleFor(x => x.StudentId)
                .NotNull().WithMessage("O ID do aluno é obrigatório.");

            RuleFor(x => x.ClassId)
                .NotNull().WithMessage("O ID da turma é obrigatório.");
        }

    }
}
