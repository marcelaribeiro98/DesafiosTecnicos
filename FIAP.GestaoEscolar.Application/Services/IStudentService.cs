using FIAP.GestaoEscolar.Domain.Requests.Student;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Student;

namespace FIAP.GestaoEscolar.Application.Services
{
    public interface IStudentService
    {
        Task<BaseResponse<int?>> CreateAsync(CreateStudentRequest request);
        Task<BaseResponse> UpdateAsync(UpdateStudentRequest request);
        Task<BaseResponse> UpdateActiveAsync(int id);
        Task<BaseResponse<GetStudentResponse?>> GetByIdAsync(int id);
        Task<BaseResponse<List<GetStudentResponse>>> GetAllAsync();
        Task<bool> UserNameExistsAsync(string username, int? id = 0);
    }
}
