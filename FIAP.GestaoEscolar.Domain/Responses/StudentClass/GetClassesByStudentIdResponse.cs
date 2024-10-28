using FIAP.GestaoEscolar.Domain.Models;

namespace FIAP.GestaoEscolar.Domain.Responses.StudentClass
{
    public class GetClassesByStudentIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Active { get; set; }

        public List<ClassModel> Classes { get; set; }
    }
}
