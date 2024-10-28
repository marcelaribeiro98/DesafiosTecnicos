using FIAP.GestaoEscolar.Domain.Requests.StudentClass;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.StudentClass;

namespace FIAP.GestaoEscolar.Application.Services
{
    public interface IStudentClassService
    {
        Task<BaseResponse> CreateAsync(CreateStudentClassRequest request);
        Task<BaseResponse> UpdateAsync(UpdateStudentClassRequest request);
        Task<BaseResponse> UpdateActiveAsync(UpdateStudentClassActiveRequest request);
        Task<BaseResponse<GetStudentClassResponse?>> GetByIdAsync(int classId, int studentId);
        Task<BaseResponse<List<GetStudentsByClassIdResponse>>> GetStudentsByClassIdAsync(int classId);
        Task<BaseResponse<List<GetClassesByStudentIdResponse>>> GetClassesByStudentIdAsync(int studentId);
    }
}
