using FIAP.GestaoEscolar.Domain.Base;

namespace FIAP.GestaoEscolar.Domain.Queries.Class
{
    public class GetClassesFilterResponse : BaseRequestPagination
    {
        public string ClassName { get; set; }
        public int Year { get; set; }
        public bool Active { get; set; }

    }
}
