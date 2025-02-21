using AutoMapper;
using MeloconjuntoApi.Dtos.Puntajes;
using MeloconjuntoApi.Models;

namespace MeloconjuntoApi.Mappers
{
    public class PuntajeMapping : Profile
    {
        public PuntajeMapping()
        {
            CreateMap<PuntajeDto, Puntaje>()
            .ForMember(dest => dest.PuntajeId, opt => opt.Ignore())
            .ForMember(dest => dest.Usuario, opt => opt.Ignore())
            .ForMember(dest => dest.Nivel, opt => opt.Ignore());

            CreateMap<Puntaje, ResponsePuntajeDto>();
        }
    }
}
