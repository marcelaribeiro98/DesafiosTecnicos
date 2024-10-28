namespace FIAP.GestaoEscolar.Domain.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
