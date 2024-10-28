using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Models.StudentClass
{
    public class ClassesByStudentIdModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool Active { get; set; }
        public List<ClassEntity> Classes { get; set; }
    }
}
