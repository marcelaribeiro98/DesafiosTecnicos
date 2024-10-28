using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Models.StudentClass;

namespace FIAP.GestaoEscolar.Infrastructure.Repositories
{
    public interface IStudentClassRepository
    {
        Task<bool?> CreateAsync(StudentClassEntity studentClassEntity);
        Task<bool?> UpdateAsync(StudentClassEntity studentClassEntity, int oldClassId);
        Task<bool?> UpdateActiveAsync(StudentClassEntity studentClassEntity);
        Task<StudentClassEntity?> GetByIdAsync(int studentId, int classId);
        Task<List<StudentClassEntity>> GetStudentClassByStudentIdAsync(int studentId);
        Task<List<StudentsByClassIdModel>> GetStudentsByClassIdAsync(int classId);
        Task<List<ClassesByStudentIdModel>> GetClassesByStudentIdAsync(int studentId);
    }
}
