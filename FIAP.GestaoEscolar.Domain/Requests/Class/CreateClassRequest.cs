namespace FIAP.GestaoEscolar.Domain.Requests.Class
{
    public class CreateClassRequest
    {
        public int CourseId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int Year { get; set; }

    }
}
