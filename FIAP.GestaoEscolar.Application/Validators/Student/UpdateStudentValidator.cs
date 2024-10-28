using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FIAP.GestaoEscolar.Domain.Requests.Student;
using FluentValidation;

namespace FIAP.GestaoEscolar.Application.Validators.Class
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentRequest>
    {
        private readonly IStudentService _studentService;
        public UpdateStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;

            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("O usuário do aluno é obrigatório.")
            .Length(1, 45).WithMessage("O usuário do aluno deve ter entre 1 e 45 caracteres.")
            .MustAsync(BeUniqueUserName).WithMessage("Esse usuário já está vinculado a outro aluno.");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do aluno é obrigatório.")
            .Length(1, 255).WithMessage("O nome do aluno deve ter entre 1 e 255 caracteres.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha do aluno é obrigatória.")
            .Length(8, 60).WithMessage("A senha do aluno deve ter entre 8 e 60 caracteres.")
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"[0-9]").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\W_]").WithMessage("A senha deve conter pelo menos um caractere especial.");
        }

        private async Task<bool> BeUniqueUserName(UpdateStudentRequest request, string username, CancellationToken cancellationToken = default)
        {
            var exists = await _studentService.UserNameExistsAsync(username, request.Id);
            return !exists;
        }
    }
}
