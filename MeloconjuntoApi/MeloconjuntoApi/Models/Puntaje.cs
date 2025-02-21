using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class Puntaje
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PuntajeId { get; set; }
        [Required]
        public int PuntajeAciertos { get; set; }
        [Required]
        public int PuntajePuntos { get; set; }
        [Required]
        public decimal PuntajeTime { get; set; }
        public int PuntajeErrores { get; set; }
        public int NivelId { get; set; }
        public int UsuarioId { get; set; }
        //Relaciones
        public virtual Usuario Usuario { get; set; }
        public virtual Nivel Nivel { get; set; }
    }
}
