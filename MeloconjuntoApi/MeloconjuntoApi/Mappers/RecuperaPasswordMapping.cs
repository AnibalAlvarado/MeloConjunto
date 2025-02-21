using AutoMapper;
using MeloconjuntoApi.Dtos.Password;
using MeloconjuntoApi.Models;

namespace MeloconjuntoApi.Mappers
{
    public class RecuperaPasswordMapping : Profile
    {
        private string GenerateActivationToken()
        {
            return Guid.NewGuid().ToString("N"); // Esto generará un token solo con letras y números
        }
        public RecuperaPasswordMapping()
        {
            CreateMap<RecuperaPasswordDto, RecuperaPassword>()

            .ForMember(dest => dest.RecuperaPasswordToken,
                      opt => opt.MapFrom(src => GenerateActivationToken()))
            .ForMember(dest => dest.RecuperaPasswordFechaLimite,
                      opt => opt.MapFrom(src => DateTime.UtcNow.AddHours(24)))
            .ForMember(dest => dest.UsuarioId,
                      opt => opt.MapFrom(src => src.UsuarioId));
        }
    }
}
