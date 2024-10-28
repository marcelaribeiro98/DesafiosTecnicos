using FIAP.GestaoEscolar.Admin.Models.Class;

namespace FIAP.GestaoEscolar.Admin.Services.Class
{
    public interface IClassService
    {
        Task<ClassModelResponse?> GetAllAsync();
    }
}
