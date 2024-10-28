using AutoMapper;
using FIAP.GestaoEscolar.Domain.Requests.Student;
using FIAP.GestaoEscolar.Domain.Responses.Student;
using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Application.Mappers
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<StudentEntity, CreateStudentRequest>()
                .ReverseMap();

            CreateMap<StudentEntity, UpdateStudentRequest>()
             .ReverseMap();

            CreateMap<StudentEntity, GetStudentResponse>()
             .ReverseMap();
        }
    }
}
