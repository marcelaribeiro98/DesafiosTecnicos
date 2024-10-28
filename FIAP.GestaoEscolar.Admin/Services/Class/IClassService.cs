using FIAP.GestaoEscolar.Admin.Models.Class;

namespace FIAP.GestaoEscolar.Admin.Services.Class
{
    public interface IClassService
    {
        Task<List<ClassModel>?> GetAllAsync();
    }
}
