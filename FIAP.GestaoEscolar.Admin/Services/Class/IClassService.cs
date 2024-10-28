using FIAP.GestaoEscolar.Admin.Models;
using FIAP.GestaoEscolar.Admin.Models.Class;

namespace FIAP.GestaoEscolar.Admin.Services.Class
{
    public interface IClassService
    {
        Task<ModelResponse?> CreateAsync(ClassModel classModel);
        Task<ModelResponse?> UpdateAsync(ClassModel classModel);
        Task<ModelResponse?> UpdateActiveAsync(int classId);
        Task<ClassModelResponse?> GetByIdAsync(int classId);
        Task<ListClassModelResponse?> GetAllAsync();
    }
}
