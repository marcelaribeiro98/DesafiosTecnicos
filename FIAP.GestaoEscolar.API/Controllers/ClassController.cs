using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Application.Validators.Class;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FIAP.GestaoEscolar.Domain.Requests.Student;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Class;
using FIAP.GestaoEscolar.Domain.Responses.Student;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/turma")]
    public class ClassController(IClassService classService) : BaseController
    {
        private readonly IClassService _classService = classService;

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateClassRequest request)
        {
            try
            {
                var validationResult = await new CreateClassValidator(_classService).ValidateAsync(request);
                if (!validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _classService.CreateAsync(request);

                return BaseResponseCreated(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(204, Type = typeof(BaseResponse))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateClassRequest request)
        {
            try
            {
                request.Id = id;

                var validationResult = await new UpdateClassValidator(_classService).ValidateAsync(request);
                if (!validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _classService.UpdateAsync(request);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpPut("{id}/ativo")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(204, Type = typeof(BaseResponse))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateActiveAsync(int id, [FromBody] bool active)
        {
            try
            {
                var response = await _classService.UpdateActiveAsync(id, active);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<GetClassResponse>))]
        [ProducesResponseType(204, Type = typeof(BaseResponse))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var response = await _classService.GetByIdAsync(id);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BaseResponse<List<GetClassResponse>>))]
        [ProducesResponseType(204, Type = typeof(BaseResponse))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _classService.GetAllAsync();
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }
    }
}
