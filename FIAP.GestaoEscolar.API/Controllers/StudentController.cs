﻿using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Application.Validators.Class;
using FIAP.GestaoEscolar.Domain.Requests.Student;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FIAP.GestaoEscolar.Domain.Responses.Student;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.GestaoEscolar.API.Controllers
{
    [ApiController]
    [Route("api/aluno")]
    public class StudentController(IStudentService studentService) : BaseController
    {
        private readonly IStudentService _studentService = studentService;

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BaseResponse<int>))]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStudentRequest request)
        {
            try
            {
                var validationResult = await new CreateStudentValidator(_studentService).ValidateAsync(request);
                if (!validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _studentService.CreateAsync(request);

                return BaseResponseCreated(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateStudentRequest request)
        {
            try
            {
                request.Id = id;

                var validationResult = await new UpdateStudentValidator(_studentService).ValidateAsync(request);
                if (!validationResult.IsValid)
                    return BaseResponseValidator(validationResult);

                var response = await _studentService.UpdateAsync(request);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpPut("{id}/ativo")]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> UpdateActiveAsync(int id)
        {
            try
            {
                var response = await _studentService.UpdateActiveAsync(id);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BaseResponse<GetStudentResponse>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var response = await _studentService.GetByIdAsync(id);
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BaseResponse<List<GetStudentResponse>>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(500, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _studentService.GetAllAsync();
                return BaseResponse(response);
            }
            catch (Exception ex)
            {
                return BaseResponseError(ex);
            }
        }
    }
}
