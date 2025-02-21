using AutoMapper;
using MeloconjuntoApi.Dtos.Usuarios;
using MeloconjuntoApi.Models;

namespace MeloconjuntoApi.Mappers
{
    public class UsuarioMapping : Profile
    {
        private string GenerateActivationToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
        public UsuarioMapping()
        {
            CreateMap<UsuarioDto, Usuario>()

            .ForMember(dest => dest.UsuarioTokenActivacion,
                      opt => opt.MapFrom(src => GenerateActivationToken()))
            .ForMember(dest => dest.UsuarioTokenVence,
                      opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(20)))
            .ForMember(dest => dest.Credenciales,
                      opt => opt.MapFrom(src => src.Credenciales));

            CreateMap<CredencialDto, Credencial>();
            CreateMap<Usuario, UsuarioResponseDto>()
           .ForMember(dest => dest.CredencialCorreo,
               opt => opt.MapFrom(src => src.Credenciales.CredencialCorreo));
        }
    }
    
}
