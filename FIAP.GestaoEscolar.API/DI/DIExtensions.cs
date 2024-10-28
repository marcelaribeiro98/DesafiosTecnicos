using FIAP.GestaoEscolar.Application.Mappers;
using FIAP.GestaoEscolar.Application.Services;
using FIAP.GestaoEscolar.Application.Services.Implementations;
using FIAP.GestaoEscolar.Infrastructure.Contexts;
using FIAP.GestaoEscolar.Infrastructure.Repositories;
using FIAP.GestaoEscolar.Infrastructure.Repositories.Implementations;

namespace FIAP.GestaoEscolar.API.DI
{
    public static class DIExtensions
    {
        public static void AddServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentClassService, StudentClassService>();

            services.AddMemoryCache(); 
        }
        public static void AddRepositoriesDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("ConnectionString não encontrada.");

            services.AddSingleton(new DBContext(connectionString));

            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentClassRepository, StudentClassRepository>();
        }
        public static void AddAutoMapperDI(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(StudentMappingProfile));
            services.AddAutoMapper(typeof(ClassMappingProfile));
            services.AddAutoMapper(typeof(StudentClassMappingProfile));
        }
    }
}
