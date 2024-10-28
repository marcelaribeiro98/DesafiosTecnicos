namespace FIAP.GestaoEscolar.Infrastructure.Entities
{
    public class StudentClassEntity
    {
        public StudentClassEntity()
        {
            Student = new StudentEntity();
            Class = new ClassEntity();
        }

        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public bool Active { get; set; }

        public StudentEntity Student { get; set; }
        public ClassEntity Class { get; set; }
    }

}
