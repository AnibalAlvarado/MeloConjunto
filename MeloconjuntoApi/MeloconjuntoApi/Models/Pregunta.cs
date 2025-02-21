using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class Pregunta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PreguntaId { get; set; }
        [Required]
        [StringLength(225)]
        public String PreguntaSentencia { get; set; }
        [Required]
        public int NivelId { get; set; }

        // Relaciones
        public virtual Nivel Nivel { get; set; }
        
        public virtual ICollection<Respuesta> Respuestas { get; set; }

    }
}
