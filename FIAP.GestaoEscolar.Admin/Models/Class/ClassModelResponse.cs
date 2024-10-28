namespace FIAP.GestaoEscolar.Admin.Models.Class
{
    public class ClassModelResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ClassModel>? Data { get;  set; }
    }
}
