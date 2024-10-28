using System.Text.Json.Serialization;

namespace FIAP.GestaoEscolar.Domain.Requests.Student
{
    public class UpdateStudentRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
