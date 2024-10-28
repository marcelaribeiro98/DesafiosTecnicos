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
        protected IActionResult BaseResponse<T>(BaseResponse<T> dados, bool created = false)
        {
            if (dados.Sucesso && !created)
                return base.Ok(dados);

            if (dados.Sucesso && created)
                return base.Created("", dados);

            if (dados.Dados == null)
                return base.NotFound(dados);

            return base.BadRequest(dados);
        }
        protected IActionResult BaseResponse(BaseResponse dados)
        {
            if (dados.Sucesso)
                return base.Ok(dados);

            return base.BadRequest(dados);
        }

        protected IActionResult BaseResponse(Exception exception)
        {
            return base.StatusCode(500, new BaseResponse(false, exception.Message));
        }
    }
}
