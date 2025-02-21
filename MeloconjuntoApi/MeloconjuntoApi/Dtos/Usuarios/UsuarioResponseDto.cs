namespace MeloconjuntoApi.Dtos.Usuarios
{
    public class UsuarioResponseDto
    {
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
        public string CredencialCorreo { get; set; }
        public bool UsuarioActivo { get; set; }
    }
}
