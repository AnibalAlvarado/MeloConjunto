using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace MeloconjuntoApi.Models
{
    public class Credencial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CredencialId { get; set; }
        public required string CredencialCorreo { get; set; }
        private string _CredencialPassword;
        public required string CredencialPassword 
        {
            get=> _CredencialPassword;
            set=> _CredencialPassword = BCrypt.Net.BCrypt.HashPassword(value,10);
        }

        public int UsuarioId { get; set; }

        // Relaciones
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
