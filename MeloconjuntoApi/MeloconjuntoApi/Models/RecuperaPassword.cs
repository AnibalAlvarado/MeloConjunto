using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MeloconjuntoApi.Models
{
    public class RecuperaPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecuperaPasswordId { get; set; }
        public string RecuperaPasswordToken { get; set; }
        [Required]
        public DateTime RecuperaPasswordFechaSolicitud { get; set; }
        [Required]
        public DateTime RecuperaPasswordFechaLimite { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [JsonIgnore]

        //Relaciones
        public virtual Usuario Usuario { get; set; }


    }
}
