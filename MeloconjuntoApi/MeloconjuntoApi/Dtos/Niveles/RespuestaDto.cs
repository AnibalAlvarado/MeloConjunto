using MeloconjuntoApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MeloconjuntoApi.Dtos.Niveles
{
    public class RespuestaDto
    {
        [Required(ErrorMessage = "El texto de la respuesta es obligatorio")]
        public required string RespuestaSentencia { get; set; }
        public required bool RespuestaCorrecta { get; set; } = false;

    }
}
