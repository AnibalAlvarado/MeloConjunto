using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class Ranking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RankingId { get; set; }

        [Required]
        public string RankingNombre { get; set; }

        // Relaciones
        public virtual ICollection<RankingUsuario> RankingsUsuarios { get; set; }

    }
}
