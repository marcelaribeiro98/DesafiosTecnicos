namespace FIAP.GestaoEscolar.Admin.Models.Class
{
    public class ClassModelResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ClassModel>? Data { get; set; }
    }
}
