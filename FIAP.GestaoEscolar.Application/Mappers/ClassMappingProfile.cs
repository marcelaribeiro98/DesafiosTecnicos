using AutoMapper;
using FIAP.GestaoEscolar.Domain.DTOs.Class;
using FIAP.GestaoEscolar.Domain.Queries.Class;
using FIAP.GestaoEscolar.Infrastructure.Entities;

namespace FIAP.GestaoEscolar.Application.Mappers
{
    public class ClassMappingProfile : Profile
    {
        public ClassMappingProfile()
        {
            CreateMap<Class, CreateClassRequest>()
                .ReverseMap();

            CreateMap<Class, UpdateClassRequest>()
             .ReverseMap();

            CreateMap<Class, GetClassResponse>()
             .ReverseMap();
        }
    }
}
