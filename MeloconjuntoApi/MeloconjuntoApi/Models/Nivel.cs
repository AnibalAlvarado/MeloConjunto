using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class Nivel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NivelId { get; set; }
        [Required]
        [StringLength(70)]
        public string NivelName { get; set; }
        [Required]
        public int MapaId {  get; set; }

        // Relaciones
        public virtual Pregunta Pregunta { get; set; }
        public virtual Mapa Mapa { get; set; }
        public virtual ICollection<Puntaje> Puntajes { get; set; }
    }
}
