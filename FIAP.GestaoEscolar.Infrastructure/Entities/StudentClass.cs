namespace FIAP.GestaoEscolar.Infrastructure.Entities
{
    public class StudentClass
    {
        public StudentClass()
        {
            Student = new Student();
            Class = new Class();
        }

        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public bool Active { get; set; }

        public Student Student { get; set; }
        public Class Class { get; set; }
    }

}
