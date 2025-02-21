namespace MeloconjuntoApi.Dtos.Niveles
{
    public class PreguntaResponseDto
    {
        public int PreguntaId { get; set; }
        public string PreguntaSentencia { get; set; }
        public List<RespuestaResponseDto> Respuestas { get; set; }
    }
}
