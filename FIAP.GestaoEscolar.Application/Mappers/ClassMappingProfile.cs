using AutoMapper;
using FIAP.GestaoEscolar.Domain.Requests.Class;
using FIAP.GestaoEscolar.Domain.Responses.Class;
using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Application.Mappers
{
    public class ClassMappingProfile : Profile
    {
        public ClassMappingProfile()
        {
            CreateMap<ClassEntity, CreateClassRequest>()
                .ReverseMap();

            CreateMap<ClassEntity, UpdateClassRequest>()
             .ReverseMap();

            CreateMap<ClassEntity, GetClassResponse>()
             .ReverseMap();
        }
    }
}
