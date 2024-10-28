namespace FIAP.GestaoEscolar.Domain.Requests.Student
{
    public class CreateStudentRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
