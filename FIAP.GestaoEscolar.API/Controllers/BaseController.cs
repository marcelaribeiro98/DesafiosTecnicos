using FIAP.GestaoEscolar.Application.Validators;
using FIAP.GestaoEscolar.Domain.Base;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.GestaoEscolar.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult BaseResponseValidator(ValidationResult validationResult)
        {
            var validationFailures = validationResult.Errors?.Select(x => new ResponseValidator() { PropertyName = x.PropertyName, ErrorMessage = x.ErrorMessage }).ToList();

            return base.BadRequest(new BaseResponse<List<ResponseValidator>?>(false, "Erro de validação", validationFailures));
        }
        protected IActionResult BaseResponse<T>(BaseResponse<T> response, bool created = false)
        {
            if (response.Success && !created)
                return base.Ok(response);

            if (response.Success && created)
                return base.Created("", response);

            if (response.Data == null)
                return base.NotFound(response);

            return base.BadRequest(response);
        }
        protected IActionResult BaseResponse(BaseResponse response)
        {
            if (response.Success)
                return base.Ok(response);

            return base.BadRequest(response);
        }

        protected IActionResult BaseResponse(Exception exception)
        {
            return base.StatusCode(500, new BaseResponse(false, exception.Message));
        }
    }
}
