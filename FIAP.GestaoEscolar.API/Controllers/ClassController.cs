using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Application.Validators.Class;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Class;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/turma")]
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;
        public ClassController(IClassService classService)
        {
            _classService = classService;

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(404, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(500, Type = typeof(BaseResponse<int>))]
        public async Task<IActionResult> PostAsync([FromBody] CreateClassRequest request)
        {
            try
            {
                var validator = new CreateClassValidator(_classService);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult != null && !validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _classService.CreateAsync(request);

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
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateClassRequest request)
        {
            try
            {
                request.Id = id;

                var validator = new UpdateClassValidator(_classService);
                var validationResult = await validator.ValidateAsync(request);

                if (validationResult != null && !validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _classService.UpdateAsync(request);
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
                var response = await _classService.UpdateActiveAsync(id, active);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<GetClassResponse?>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse<GetClassResponse?>))]
        [ProducesResponseType(404, Type = typeof(BaseResponse<GetClassResponse?>))]
        [ProducesResponseType(500, Type = typeof(BaseResponse<GetClassResponse?>))]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var response = await _classService.GetByIdAsync(id);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BaseResponse<List<GetClassResponse>>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse<List<GetClassResponse>>))]
        [ProducesResponseType(404, Type = typeof(BaseResponse<List<GetClassResponse>>))]
        [ProducesResponseType(500, Type = typeof(BaseResponse<List<GetClassResponse>>))]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _classService.GetAllAsync();
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponse(ex);
            }
        }
    }
}
