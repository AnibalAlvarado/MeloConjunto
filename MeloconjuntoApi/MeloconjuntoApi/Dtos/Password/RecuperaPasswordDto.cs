using MeloconjuntoApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MeloconjuntoApi.Dtos.Password
{
    public class RecuperaPasswordDto
    {
        [Required]
        [EmailAddress]
        public string RecuperaPasswordCorreo { get; set; }
        public DateTime RecuperaPasswordFechaSolicitud { get; set; }
        public DateTime RecuperaPasswordFechaLimite { get; set; }
        [JsonIgnore]
        public int UsuarioId { get; set; }

    }
}
