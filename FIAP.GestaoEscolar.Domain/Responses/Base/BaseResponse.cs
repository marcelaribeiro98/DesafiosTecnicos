namespace FIAP.GestaoEscolar.Domain.Responses.Base
{
    public class BaseResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse(bool success, string message, T? data = default) : base(success, message)
        {
            Data = data;
        }

        public T? Data { get; private set; }
    }
    public class BasePaginationResponse<T> : BaseResponse
    {
        public BasePaginationResponse(bool success, string message, List<T>? items, int? totalItems = 0, int? totalDisplayedItems = 0, int? totalPages = 0, int? currentPage = 0) : base(success, message)
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
