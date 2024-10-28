using System.ComponentModel.DataAnnotations;

namespace FIAP.GestaoEscolar.Admin.Models.Student
{
    public class StudentModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
        [StringLength(255, MinimumLength = 1, ErrorMessage = "O nome do usuário deve ter entre 1 e 255 caracteres.")]

        public string Name { get; set; } = string.Empty;

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "O nome do usuário deve ter entre 1 e 45 caracteres.")]

        public string Username { get; set; } = string.Empty;

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 15 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]

        public string Password { get; set; } = string.Empty;
        [Display(Name = "Ativo")]
        public bool Active { get; set; }
    }
}
