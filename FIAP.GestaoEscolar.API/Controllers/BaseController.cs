using FIAP.GestaoEscolar.Application.Validators;
using FIAP.GestaoEscolar.Domain.Responses.Base;
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
        protected IActionResult BaseResponse(BaseResponse response)
        {
            if (response.Success)
                return base.Ok(response);

            return base.BadRequest(response);
        }

        protected IActionResult BaseResponse<T>(BaseResponse<T> response)
        {
            if (response.Success)
                return base.Ok(response);

            if (response.Data == null)
                return base.NoContent();

            return base.BadRequest(response);
        }

        protected IActionResult BaseResponseCreated<T>(BaseResponse<T> response)
        {
            if (response.Success)
                return base.Created("", response);

            return base.BadRequest(response);
        }

        protected IActionResult BaseResponseError(Exception exception)
        {
            return base.StatusCode(500, new BaseResponse(false, exception.Message));
        }
    }
}
