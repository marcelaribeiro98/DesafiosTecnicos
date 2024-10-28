using AutoMapper;
using FIAP.GestaoEscolar.Application.Services.Implementations;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace FIAP.GestaoEscolar.Tests.Class
{
    public class ClassServiceTests
    {
        private readonly Mock<IClassRepository> _mockClassRepository;
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClassService _classService;
        public ClassServiceTests()
        {
            _mockClassRepository = new Mock<IClassRepository>();
            _mockCache = new Mock<IMemoryCache>();
            _mockMapper = new Mock<IMapper>();
            _classService = new ClassService(_mockClassRepository.Object, _mockCache.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task MustCreateClass()
        {
            var request = new CreateClassRequest { ClassName = "Turma A", Year = 2023 };
            var entityMapper = new ClassEntity { Id = 1 }; 

            _mockMapper.Setup(m => m.Map<ClassEntity>(request)).Returns(entityMapper);
            _mockClassRepository.Setup(repo => repo.CreateAsync(entityMapper)).ReturnsAsync(1); 

            var result = await _classService.CreateAsync(request);

            Assert.True(result.Success);
            Assert.Equal("Turma cadastrada com sucesso.", result.Message);
            Assert.Equal(1, result.Data);
        }

    }
}
