using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class Dificultad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DificultadId { get; set; }

        [Required]
        [StringLength(30)]
        public string DificultadNombre { get; set; }

        //Relaciones
        public virtual ICollection<Mapa> Mapas { get; set; }
    }
}
