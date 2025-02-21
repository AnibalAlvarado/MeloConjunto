using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class UsuarioRol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioRolId { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }

        // Relaciones
        public virtual Usuario Usuario { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
