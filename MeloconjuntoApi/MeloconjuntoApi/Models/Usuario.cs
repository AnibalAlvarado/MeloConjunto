using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MeloconjuntoApi.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        public required string UsuarioNombre { get; set; }
        public required string UsuarioApellido { get; set; }
        public required byte UsuarioEdad { get; set; }
        public DateTime UsuarioFechaRegistro { get; set; }
        public bool UsuarioActivo { get; set; } = false;

        public required String? UsuarioTokenActivacion { get; set; }
        public required DateTime UsuarioTokenVence { get; set; }

        //Relaciones
        public virtual Credencial Credenciales { get; set; }
        public virtual ICollection<RecuperaPassword> RecuperarPasswords{ get; set; }
        public virtual ICollection<Puntaje> Puntajes { get; set; }
        public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; }
        public virtual ICollection<RankingUsuario> RankingsUsuarios {  get; set; }
    }

}
