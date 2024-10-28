using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories
{
    public interface IClassRepository
    {
        Task<int?> CreateAsync(ClassEntity classEntity);
        Task<bool?> UpdateAsync(ClassEntity classEntity);
        Task<bool?> UpdateActiveAsync(int id, bool active);
        Task<ClassEntity?> GetByIdAsync(int id);
        Task<List<ClassEntity>?> GetAllAsync();
        Task<bool?> ClassNameExistsAsync(string className, int? id = 0);
    }
}
