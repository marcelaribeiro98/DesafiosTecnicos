namespace FIAP.GestaoEscolar.Infrastructure.Entities
{
    public class StudentClass
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }

        public Student? Aluno { get; set; }
        public Class? Turma { get; set; }
    }

}
