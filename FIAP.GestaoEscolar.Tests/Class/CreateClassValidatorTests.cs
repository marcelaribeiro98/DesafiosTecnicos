using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Application.Validators.Class;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FIAP.GestaoEscolar.Domain.Responses.Base;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP.GestaoEscolar.Tests.Class
{
    public class CreateClassValidatorTests
    {
        private readonly Mock<IClassService> _mockClassService;
        private readonly CreateClassValidator _validator;

        public CreateClassValidatorTests()
        {
            _mockClassService = new Mock<IClassService>();
            _validator = new CreateClassValidator(_mockClassService.Object);
        }

        [Fact]
        public async Task MustValidateEmptyCourseId()
        {
            var request = new CreateClassRequest { CourseId = 0, ClassName = "Turma A", Year = 2023 };

            var result = await _validator.ValidateAsync(request);

            Assert.False(result.IsValid);
            Assert.Equal("O id do curso é obrigatório.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public async Task MustValidateClassName()
        {
            var request = new CreateClassRequest { CourseId = 1, ClassName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", Year = 2023 };

            var result = await _validator.ValidateAsync(request);

            Assert.False(result.IsValid);
            Assert.Equal("O nome da turma deve ter entre 1 e 45 caracteres.", result.Errors[0].ErrorMessage);
        }
    }
}
