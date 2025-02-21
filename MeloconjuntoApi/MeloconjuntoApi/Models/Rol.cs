using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeloconjuntoApi.Models
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolId { get; set; }
        [Required]
        [MaxLength(50)]
        public string RolNombre { get; set; }

        // Relaciones
        public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; }
    }
}
