using AutoMapper;
using MeloconjuntoApi.Dtos.Dificultades;
using MeloconjuntoApi.Dtos.Mapas;
using MeloconjuntoApi.Models;

namespace MeloconjuntoApi.Mappers
{
    public class MapaMapping : Profile
    {
        public MapaMapping()
        {
            CreateMap<MapaDto, Mapa>();

            CreateMap<Mapa, GetMapaDto>();

            CreateMap<Dificultad, DificultadDto>();

        }
    }
}
