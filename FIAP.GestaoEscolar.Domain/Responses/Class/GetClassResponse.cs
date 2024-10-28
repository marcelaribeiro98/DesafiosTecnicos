namespace FIAP.GestaoEscolar.Domain.Responses.Class
{
    public class GetClassResponse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool Active { get; set; }
    }
}
