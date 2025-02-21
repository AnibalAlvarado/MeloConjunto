using MeloconjuntoApi.Dtos.Dificultades;

namespace MeloconjuntoApi.Dtos.Mapas
{
    public class GetMapaDto
    {
        public required int MapaId { get; set; }
        public string MapaNombre { get; set; }

        public required int DificultadId { get; set; }
        // ... otras propiedades
        public DificultadDto Dificultad { get; set; }
    }
}
