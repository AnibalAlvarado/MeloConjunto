using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MeloconjuntoApi.Models
{
    public class Respuesta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RespuestaId { get; set; }
        [Required]
        [StringLength(225)]
        public string RespuestaSentencia { get; set; }
        [Required]
        public bool RespuestaCorrecta { get; set; }=false;

        [Required]
        public int PreguntaId { get; set; }

        // Relaciones
        [JsonIgnore]
        public virtual Pregunta Pregunta { get; set; }
    }
}
