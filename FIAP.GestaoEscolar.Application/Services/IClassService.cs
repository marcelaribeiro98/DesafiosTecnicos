using FIAP.GestaoEscolar.Domain.Base;
using FIAP.GestaoEscolar.Domain.DTOs.Class;
using FIAP.GestaoEscolar.Domain.Queries.Class;

namespace FIAP.GestaoEscolar.Application.Services
{
    public interface IClassService
    {
        Task<BaseResponse<int>> CreateAsync(CreateClassRequest request);
        Task<BaseResponse> UpdateAsync(UpdateClassRequest request);
        Task<BaseResponse> UpdateActiveAsync(int id, bool active);
        Task<BaseResponse<GetClassResponse?>> GetByIdAsync(int id);
        Task<BaseResponse<List<GetClassResponse>>> GetAllAsync();
        Task<bool> ClassNameExistsAsync(string className, int? id = 0);
    }
}
