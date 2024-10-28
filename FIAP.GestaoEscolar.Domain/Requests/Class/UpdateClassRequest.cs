using System.Text.Json.Serialization;

namespace FIAP.GestaoEscolar.Domain.Requests.Class
{
    public class UpdateClassRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int Year { get; set; }
        public bool Active { get; set; }
    }
}
