namespace MeloconjuntoApi.Dtos.Niveles
{
    public class NivelResponseDto
    {
        public int NivelId { get; set; }
        public string NivelName { get; set; }
        public int MapaId { get; set; }
        public PreguntaResponseDto Pregunta { get; set; }
    }
}
