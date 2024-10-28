namespace FIAP.GestaoEscolar.Domain.Base
{
    public class BaseResponse
    {
        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }
        public BaseResponse(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse(bool sucesso, string mensagem, T? dados) : base(sucesso, mensagem)
        {
            Dados = dados;
        }

        public T? Dados { get; private set; }
    }
    public class BasePaginationResponse<T> : BaseResponse
    {
        public BasePaginationResponse(bool sucesso, string mensagem, List<T>? items, int? totalItems = 0, int? totalDisplayedItems = 0, int? totalPages = 0, int? currentPage = 0) : base(sucesso, mensagem)
        {
            TotalItems = totalItems;
            TotalDisplayedItems = totalDisplayedItems;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            Items = items;
        }

        public int? TotalItems { get; private set; }
        public int? TotalDisplayedItems { get; private set; }
        public int? TotalPages { get; private set; }
        public int? CurrentPage { get; private set; }
        public List<T>? Items { get; private set; }
    }
}
