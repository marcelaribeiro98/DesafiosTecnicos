using FIAP.GestaoEscolar.Domain.Responses.Base;

namespace FIAP.GestaoEscolar.Domain.Responses.Class
{
    public class GetClassesFilterResponse : BaseRequestPagination
    {
        public string ClassName { get; set; }
        public int Year { get; set; }
        public bool Active { get; set; }

    }
}
