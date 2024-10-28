using FIAP.GestaoEscolar.Admin.Models.Options;
using FIAP.GestaoEscolar.Admin.Services.Class;
using FIAP.GestaoEscolar.Admin.Services.Class.Implementations;
using FIAP.GestaoEscolar.Admin.Services.Student;
using FIAP.GestaoEscolar.Admin.Services.Student.Implementations;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Usar camelCase
                });

builder.Services.Configure<ApiSettings>(builder.Configuration);

builder.Services.AddHttpClient();

builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
