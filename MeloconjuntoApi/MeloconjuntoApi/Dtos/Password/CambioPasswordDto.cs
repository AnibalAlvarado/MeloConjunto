using System.ComponentModel.DataAnnotations;

namespace MeloconjuntoApi.Dtos.Password
{
    public class CambioPasswordDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
