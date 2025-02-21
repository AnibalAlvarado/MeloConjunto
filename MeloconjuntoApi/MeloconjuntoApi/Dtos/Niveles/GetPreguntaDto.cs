namespace MeloconjuntoApi.Dtos.Niveles
{
    public class GetPreguntaDto
    {
        public string PreguntaId { get; set; }
        public string PreguntaSentencia { get; set; }

        public RespuestaDto Preguntas { get; set; }
    }
}
