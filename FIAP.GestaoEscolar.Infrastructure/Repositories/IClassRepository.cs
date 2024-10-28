using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories
{
    public interface IClassRepository
    {
        Task<int?> CreateAsync(Class classEntity);
        Task<bool?> UpdateAsync(Class classEntity);
        Task<bool?> UpdateActiveAsync(int id, bool active);
        Task<Class?> GetByIdAsync(int id);
        Task<List<Class>?> GetAllAsync();
        Task<bool?> ClassNameExistsAsync(string className, int? id = 0);
    }
}
