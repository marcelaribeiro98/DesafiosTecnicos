using FIAP.GestaoEscolar.API.DI;
using FIAP.GestaoEscolar.Application.Validators.Class;
using FluentValidation;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentValidator>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //DI Extensions

        builder.Services.AddServicesDI();
        builder.Services.AddRepositoriesDI(builder.Configuration);
        builder.Services.AddAutoMapperDI();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}