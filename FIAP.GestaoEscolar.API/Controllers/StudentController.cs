using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Application.Validators.Class;
using FIAP.GestaoEscolar.Domain.Requests.Student;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Student;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(404, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(500, Type = typeof(BaseResponse<int>))]
        public async Task<IActionResult> PostAsync([FromBody] CreateStudentRequest request)
        {
            try
            {
                var validator = new CreateStudentValidator(_studentService);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult != null && !validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _studentService.CreateAsync(request);

                return BaseResponse(response, true);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(404, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateStudentRequest request)
        {
            try
            {
                request.Id = id;

                var validator = new UpdateStudentValidator(_studentService);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult != null && !validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _studentService.UpdateAsync(request);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }

        [HttpPut("{id}/ativo")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(404, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> PutActiveAsync(int id, [FromBody] bool active)
        {
            try
            {
                var response = await _studentService.UpdateActiveAsync(id, active);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<GetStudentResponse?>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse<GetStudentResponse?>))]
        [ProducesResponseType(404, Type = typeof(BaseResponse<GetStudentResponse?>))]
        [ProducesResponseType(500, Type = typeof(BaseResponse<GetStudentResponse?>))]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var response = await _studentService.GetByIdAsync(id);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BaseResponse<List<GetStudentResponse>>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse<List<GetStudentResponse>>))]
        [ProducesResponseType(404, Type = typeof(BaseResponse<List<GetStudentResponse>>))]
        [ProducesResponseType(500, Type = typeof(BaseResponse<List<GetStudentResponse>>))]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _studentService.GetAllAsync();
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }
    }
}
