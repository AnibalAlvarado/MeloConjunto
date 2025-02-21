using System.ComponentModel.DataAnnotations;

namespace MeloconjuntoApi.Dtos.Niveles
{
    public class NivelDto
    {
        [Required(ErrorMessage = "El nombre del nivel es obligatorio")]
        [StringLength(70, MinimumLength = 1)]
        public required string NivelName { get; set; }
        [Required(ErrorMessage = "El ID del mapa es obligatorio")]
        public required int MapaId { get; set; }

        [Required(ErrorMessage = "La pregunta es obligatoria")]
        public required PreguntaDto Pregunta { get; set; }
    }
}
