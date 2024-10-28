namespace FIAP.GestaoEscolar.Infrastructure.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
