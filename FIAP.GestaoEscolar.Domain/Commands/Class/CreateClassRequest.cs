namespace FIAP.GestaoEscolar.Domain.DTOs.Class
{
    public class CreateClassRequest
    {
        public int CourseId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int Year { get; set; }

    }
}
