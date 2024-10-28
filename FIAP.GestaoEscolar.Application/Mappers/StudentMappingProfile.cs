using AutoMapper;
using FIAP.GestaoEscolar.Domain.Commands.Student;
using FIAP.GestaoEscolar.Domain.DTOs.Student;
using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Application.Mappers
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, CreateStudentRequest>()
                .ReverseMap();

            CreateMap<Student, UpdateStudentRequest>()
             .ReverseMap();
        }
    }
}
