using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class Mapa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MapaId { get; set; }
        [Required]
        [StringLength(50)]
        public string MapaNombre { get; set; }

        [Required]
        public int DificultadId { get; set; }
        [Required]

        // Relaciones
        public virtual Dificultad Dificultad { get; set; }
        public virtual ICollection<Nivel> Niveles { get; set; }


    }
}
