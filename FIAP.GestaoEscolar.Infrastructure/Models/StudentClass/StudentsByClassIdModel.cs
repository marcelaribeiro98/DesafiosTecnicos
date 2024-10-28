using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Models.StudentClass
{
    public class StudentsByClassIdModel
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int CourseId{ get; set; }
        public int Year { get; set; }
        public bool Active { get; set; }

        public List<Student> Students { get; set; }
    }
}
