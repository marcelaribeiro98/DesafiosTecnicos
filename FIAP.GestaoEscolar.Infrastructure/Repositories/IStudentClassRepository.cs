using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Models.StudentClass;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories
{
    public interface IStudentClassRepository
    {
        Task<bool?> CreateAsync(StudentClass studentClassEntity);
        Task<bool?> UpdateAsync(StudentClass studentClassEntity, int oldClassId);
        Task<bool?> UpdateActiveAsync(StudentClass studentClassEntity);
        Task<StudentClass?> GetByIdAsync(int studentId, int classId);
        Task<List<StudentClass>> GetStudentClassByStudentIdAsync(int studentId);
        Task<List<StudentsByClassIdModel>> GetStudentsByClassIdAsync(int classId);
        Task<List<ClassesByStudentIdModel>> GetClassesByStudentIdAsync(int studentId);
    }
}
