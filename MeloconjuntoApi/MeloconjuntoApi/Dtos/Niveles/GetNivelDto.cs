using System.ComponentModel.DataAnnotations;

namespace MeloconjuntoApi.Dtos.Niveles
{
    public class GetNivelDto
    {
        public int NivelId { get; set; }
        public string NivelName { get; set; }
        public int MapaId { get; set; }

        public PreguntaDto Preguntas { get; set; }
        public List<RespuestaDto> Respuestas { get; set; }
    }
}
