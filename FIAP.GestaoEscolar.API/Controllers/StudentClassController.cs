using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Application.Validators.StudentClass;
using FIAP.GestaoEscolar.Domain.Requests.StudentClass;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.StudentClass;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/alunoturma")]
    public class StudentClassController(IStudentClassService studentClassService) : BaseController
    {
        private readonly IStudentClassService _studentClassService = studentClassService;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStudentClassRequest request)
        {
            try
            {
                var validationResult = await new CreateStudentClassValidator().ValidateAsync(request);
                if (!validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _studentClassService.CreateAsync(request);

                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateStudentClassRequest request)
        {
            try
            {
                var validationResult = await new UpdateStudentClassValidator().ValidateAsync(request);
                if (!validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _studentClassService.UpdateAsync(request);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpPut("ativo")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateActiveAsync([FromBody] UpdateStudentClassActiveRequest request)
        {
            try
            {
                var response = await _studentClassService.UpdateActiveAsync(request);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpGet("aluno/{alunoId}/turma/{turmaId}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<GetStudentClassResponse>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetByIdAsync(int alunoId, int turmaId)
        {
            try
            {
                var response = await _studentClassService.GetByIdAsync(alunoId, turmaId);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }


        [HttpGet("/turma/{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<List<GetStudentsByClassIdResponse>>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetStudentsByClassIdAsync(int id)
        {
            try
            {
                var response = await _studentClassService.GetStudentsByClassIdAsync(id);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpGet("/aluno/{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<List<GetClassesByStudentIdResponse>>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetClassesByStudentIdAsync(int id)
        {
            try
            {
                var response = await _studentClassService.GetClassesByStudentIdAsync(id);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }
    }
}
