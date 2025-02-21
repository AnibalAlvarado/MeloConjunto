using System.ComponentModel.DataAnnotations;

namespace MeloconjuntoApi.Dtos.Mapas
{
    public class MapaDto
    {
        [Required(ErrorMessage = "El mapa debe tener un nombre")]
        [StringLength(50, MinimumLength = 1)]
        public required string MapaNombre { get; set; }

        [Required(ErrorMessage = "El mapa debe tener una dificultad")]
        public required short DificultadId { get; set; }
    }
}
