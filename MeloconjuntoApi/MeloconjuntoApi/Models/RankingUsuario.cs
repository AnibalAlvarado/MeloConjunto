using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class RankingUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RankingUsuarioId { get; set; }
        public int RankingId { get; set; }
        public int UsuarioId { get; set; }

        // Relaciones
        public virtual Usuario Usuario { get; set; }
        public virtual Ranking Ranking { get; set; }

    }
}
