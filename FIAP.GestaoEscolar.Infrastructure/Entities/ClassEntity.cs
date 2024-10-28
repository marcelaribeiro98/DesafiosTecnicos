namespace FIAP.GestaoEscolar.Infrastructure.Entities
{
    public class ClassEntity
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? ClassName { get; set; }
        public int Year { get; set; }
        public bool Active { get; set; }

    }
}
