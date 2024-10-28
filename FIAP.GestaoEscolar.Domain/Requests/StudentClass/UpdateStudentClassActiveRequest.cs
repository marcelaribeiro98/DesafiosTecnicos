namespace FIAP.GestaoEscolar.Domain.Requests.StudentClass
{
    public class UpdateStudentClassActiveRequest
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public bool Active { get; set; }
    }
}
