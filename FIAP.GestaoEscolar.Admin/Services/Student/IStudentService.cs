using FIAP.GestaoEscolar.Admin.Models;
using FIAP.GestaoEscolar.Admin.Models.Student;

namespace FIAP.GestaoEscolar.Admin.Services.Student
{
    public interface IStudentService
    {
        Task<ModelResponse?> CreateAsync(StudentModel studentModel);
        Task<ModelResponse?> UpdateAsync(StudentModel studentModel);
        Task<ModelResponse?> UpdateActiveAsync(int studentId);
        Task<StudentModelResponse?> GetByIdAsync(int studentId);
        Task<ListStudentModelResponse?> GetAllAsync();
    }
}
