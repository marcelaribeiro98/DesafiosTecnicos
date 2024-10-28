namespace FIAP.GestaoEscolar.Admin.Models.Class
{
    public class ClassModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool Active { get; set; }
    }
}
