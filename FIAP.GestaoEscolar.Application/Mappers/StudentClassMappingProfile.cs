using AutoMapper;
using FIAP.GestaoEscolar.Domain.Models;
using FIAP.GestaoEscolar.Domain.Requests.StudentClass;
using FIAP.GestaoEscolar.Domain.Responses.StudentClass;
using FIAP.GestaoEscolar.Infrastructure.Entities;
using FIAP.GestaoEscolar.Infrastructure.Models.StudentClass;

namespace FIAP.GestaoEscolar.Application.Mappers
{
    public class StudentClassMappingProfile : Profile
    {
        public StudentClassMappingProfile()
        {
            CreateMap<StudentClassEntity, CreateStudentClassRequest>()
                .ReverseMap();

            CreateMap<StudentClassEntity, UpdateStudentClassRequest>()
             .ReverseMap();

            CreateMap<StudentModel, StudentEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Active, opt => opt.Ignore())
             .ReverseMap();

            CreateMap<ClassModel, ClassEntity>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Active, opt => opt.Ignore())
             .ReverseMap();

            CreateMap<StudentClassEntity, GetStudentClassResponse>()
             .ReverseMap();

            CreateMap<StudentsByClassIdModel, GetStudentsByClassIdResponse>()
             .ReverseMap();

            CreateMap<ClassesByStudentIdModel, GetClassesByStudentIdResponse>()
             .ReverseMap();

        }
    }
}
