using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories
{
    public interface IStudentRepository
    {
        Task<int?> CreateAsync(Student studentEntity);
        Task<bool?> UpdateAsync(Student studentEntity);
        Task<bool?> UpdateActiveAsync(int id, bool active);
        Task<Student?> GetByIdAsync(int id);
        Task<List<Student>?> GetAllAsync();
        Task<bool?> UserNameExistsAsync(string username, int? id = 0);
    }
}
