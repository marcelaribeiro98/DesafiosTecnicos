using System.ComponentModel.DataAnnotations;

namespace FIAP.GestaoEscolar.Admin.Models.Class
{
    public class ClassModel
    {
        public int Id { get; set; }

        [Display(Name = "Curso Id")]
        [Required(ErrorMessage = "O id do curso é obrigatório.")]
        public int CourseId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome da turma é obrigatório.")]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "O nome da turma deve ter entre 1 e 45 caracteres.")]
        public string ClassName { get; set; } = string.Empty;

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "O ano da turma é obrigatório.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "O ano deve ter exatamente 4 dígitos.")]
        [Range(1, 9999, ErrorMessage = "O ano deve ser um número positivo entre 1 e 9999.")]
        public int Year { get; set; }

        [Display(Name = "Ativo")]
        public bool Active { get; set; }
    }
}
