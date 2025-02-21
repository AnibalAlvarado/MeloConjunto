namespace MeloconjuntoApi.Dtos.Usuarios
{
    public class LoginDto
    {
        public required string CredencialCorreo { get; set; }
        public required string CredencialPassword { get; set; }
    }
}
