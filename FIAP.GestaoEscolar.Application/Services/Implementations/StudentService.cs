using FIAP.GestaoEscolar.Domain.Requests.Student;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Student;

namespace FIAP.GestaoEscolar.Application.Services.Implementations
{
    public class StudentService : IStudentService
    {
        public StudentService()
        {
                
        }

        public Task<BaseResponse<int>> CreateAsync(CreateStudentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<GetStudentResponse>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<GetStudentResponse?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateActiveAsync(int id, bool active)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateAsync(UpdateStudentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserNameExistsAsync(string className, int? id = 0)
        {
            throw new NotImplementedException();
        }
    }
}
