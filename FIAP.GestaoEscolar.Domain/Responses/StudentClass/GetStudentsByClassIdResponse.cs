using FIAP.GestaoEscolar.Domain.Models;

namespace FIAP.GestaoEscolar.Domain.Responses.StudentClass
{
    public class GetStudentsByClassIdResponse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? ClassName { get; set; }
        public int Year { get; set; }
        public bool Active { get; set; }

        public List<StudentModel> Students { get; set; }
    }
}
