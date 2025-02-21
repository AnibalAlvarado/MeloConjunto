using MeloconjuntoApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MeloconjuntoApi.Dtos.Niveles
{
    public class PreguntaDto
    {
        [Required(ErrorMessage = "El texto de la pregunta es obligatorio")]
        public required string PreguntaSentencia { get; set; }

        [Required(ErrorMessage = "Se requieren Máximo 4 respuestas")]
        [MinLength(3, ErrorMessage = "Se requieren mínimo 3 respuestas")]
        [MaxLength(4, ErrorMessage = "Máximo 4 respuestas")]
        public required List<RespuestaDto> Respuestas { get; set; }

    }
}
