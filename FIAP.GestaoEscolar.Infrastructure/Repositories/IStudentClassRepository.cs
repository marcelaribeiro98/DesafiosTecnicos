using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories
{
    public interface IStudentClassRepository
    {
        Task<int> CreateAsync(StudentClass studentClassEntity);
        Task<bool> UpdateAsync(StudentClass studentClassEntity);
        Task<List<StudentClass>> GetAllAsync();
    }
}
