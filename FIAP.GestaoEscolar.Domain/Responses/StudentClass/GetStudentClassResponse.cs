using FIAP.GestaoEscolar.Domain.Responses.Class;

namespace FIAP.GestaoEscolar.Domain.Responses.StudentClass
{
    public class GetStudentClassResponse
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public bool Active { get; set; }

        public GetStudentClassResponse Student { get; set; }
        public GetClassResponse Class { get; set; }
    }
}
