using FIAP.GestaoEscolar.Domain.Base;
using FIAP.GestaoEscolar.Domain.Commands.Student;
using FIAP.GestaoEscolar.Domain.Queries.Student;

namespace FIAP.GestaoEscolar.Application.Services
{
    public interface IStudentService
    {
        Task<BaseResponse<int>> CreateAsync(CreateStudentRequest request);
        Task<BaseResponse> UpdateAsync(UpdateStudentRequest request);
        Task<BaseResponse> UpdateActiveAsync(int id, bool active);
        Task<BaseResponse<GetStudentResponse?>> GetByIdAsync(int id);
        Task<BaseResponse<List<GetStudentResponse>>> GetAllAsync();
        Task<bool> UserNameExistsAsync(string className, int? id = 0);
    }
}
