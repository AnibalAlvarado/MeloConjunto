using AutoMapper;
using MeloconjuntoApi.Dtos.Niveles;
using MeloconjuntoApi.Models;

namespace MeloconjuntoApi.Mappers
{
    public class NivelMapping : Profile
    {
        public NivelMapping() 
        {
            CreateMap<NivelDto, Nivel>()
            .ForMember(dest => dest.Pregunta, opt => opt.Ignore());

            CreateMap<PreguntaDto, Pregunta>()
                .ForMember(dest => dest.Respuestas, opt => opt.Ignore());

            CreateMap<RespuestaDto, Respuesta>();

            CreateMap<Nivel, GetNivelDificultadDto>();
            CreateMap<Nivel, NivelResponseDto>();
            CreateMap<Pregunta, PreguntaResponseDto>();
            CreateMap<Respuesta, RespuestaResponseDto>();
        }
    }
}
