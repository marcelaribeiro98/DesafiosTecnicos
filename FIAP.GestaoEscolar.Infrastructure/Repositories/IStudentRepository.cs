using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories
{
    public interface IStudentRepository
    {
        Task<int?> CreateAsync(StudentEntity studentEntity);
        Task<bool?> UpdateAsync(StudentEntity studentEntity);
        Task<bool?> UpdateActiveAsync(int id, bool active);
        Task<StudentEntity?> GetByIdAsync(int id);
        Task<List<StudentEntity>?> GetAllAsync();
        Task<bool?> UserNameExistsAsync(string username, int? id = 0);
    }
}
