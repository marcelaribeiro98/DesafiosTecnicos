using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FluentValidation;

namespace FIAP.GestaoEscolar.Application.Validators.Class
{
    public class CreateClassValidator : AbstractValidator<CreateClassRequest>
    {
        private readonly IClassService _classService;
        public CreateClassValidator(IClassService classService)
        {
            _classService = classService;

            RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("O id do curso é obrigatório.")
            .GreaterThan(0).WithMessage("O Id do curso deve ser um número positivo.");

            RuleFor(x => x.ClassName)
            .NotEmpty().WithMessage("O nome da turma é obrigatório.")
            .Length(1, 45).WithMessage("O nome da turma deve ter entre 1 e 45 caracteres.")
            .MustAsync(BeUniqueClassName).WithMessage("O nome da turma já existe.");

            RuleFor(turma => turma.Year)
            .GreaterThan(0).WithMessage("O ano deve ser um número positivo.")
            .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano não pode ser maior que o ano atual.")
            .Must(BeFourDigits).WithMessage("O ano deve ter exatamente 4 dígitos.");
        }
        private async Task<bool> BeUniqueClassName(string className, CancellationToken cancellationToken = default)
        {
            var exists = await _classService.ClassNameExistsAsync(className);
            return !exists;
        }

        private bool BeFourDigits(int year)
        {
            return year.ToString().Length == 4;
        }
    }
}
