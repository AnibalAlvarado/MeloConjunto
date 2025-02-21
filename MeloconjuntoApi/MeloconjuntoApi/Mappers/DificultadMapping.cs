using AutoMapper;
using MeloconjuntoApi.Dtos.Dificultades;
using MeloconjuntoApi.Models;

namespace MeloconjuntoApi.Mappers
{
    public class DificultadMapping : Profile
    {
        public DificultadMapping()
        {
            CreateMap<Dificultad, GetDificultadDto>();

            CreateMap<Dificultad, DificultadDto>()
            .ForMember(dest => dest.DificultadNombre, opt => opt.MapFrom(src => src.DificultadNombre));

            CreateMap<DificultadDto, Dificultad>()
            .ForMember(dest => dest.DificultadNombre, opt => opt.MapFrom(src => src.DificultadNombre))
            .ForMember(dest => dest.DificultadId, opt => opt.Ignore())
            .ForMember(dest => dest.Mapas, opt => opt.Ignore());
            

        }
    }
}
