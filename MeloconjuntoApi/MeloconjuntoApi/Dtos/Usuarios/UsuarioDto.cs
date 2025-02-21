namespace MeloconjuntoApi.Dtos.Usuarios
{
    public class UsuarioDto
    {
        public required string UsuarioNombre { get; set; }
        public required string UsuarioApellido { get; set; }
        public required byte UsuarioEdad { get; set; }
        public DateTime UsuarioFechaRegistro { get; set; }
        public required CredencialDto Credenciales { get; set; }
    }
}
