using System.ComponentModel.DataAnnotations;

namespace MeloconjuntoApi.Dtos.Puntajes
{
    public class PuntajeDto
    {
        [Required]
        public int PuntajeAciertos { get; set; }

        [Required]
        public int PuntajePuntos { get; set; }

        [Required]
        public decimal PuntajeTime { get; set; }

        public int PuntajeErrores { get; set; }

        [Required]
        public int NivelId { get; set; }

        [Required]
        public int UsuarioId { get; set; }
    }
}
