using AutoMapper;
using MeloconjuntoApi.Data.Context;
using MeloconjuntoApi.Dtos.Usuarios;
using MeloconjuntoApi.Models;
using MeloconjuntoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace MeloconjuntoApi.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;

        public UsuarioController(ILogger<UsuarioController> logger, DataContext context, IMapper mapper, EmailService emailService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        //Registro de usuario

        [HttpPost("Register")]
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] UsuarioDto usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Usuario existingUser = await _context.Usuarios
            .Include(u => u.Credenciales)
            .FirstOrDefaultAsync(u => u.Credenciales.CredencialCorreo == usuarioDto.Credenciales.CredencialCorreo);

            if (existingUser != null)
            {
                return BadRequest(new { message = "El correo electrónico ya está registrado" });
            }


            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

            string  subjectEmail = "Activa tu cuenta";
            string bodyEmail = $"Hola {usuarioDto.UsuarioNombre},\n\nPor favor, activa tu cuenta usando el siguiente Codigo:{usuario.UsuarioTokenActivacion} ";
            try
            {
                await _emailService.EnviarEmailAsync(usuario.Credenciales.CredencialCorreo, subjectEmail, bodyEmail);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "No se pudo enviar el correo de verificación. Por favor, verifica la dirección de correo." });
            }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            

            usuario.Credenciales.CredencialPassword = "[PROTECTED]";

            return Ok("Usuario creado exitosamente");
        }

        // Login de usuario

        [HttpPost("Login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginDto loginDto)
        {
            Usuario usuario = await _context.Usuarios
                .Include(u => u.Credenciales)
                .FirstOrDefaultAsync(u => u.Credenciales.CredencialCorreo == loginDto.CredencialCorreo);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos." });
            }

            if (usuario.UsuarioActivo == false)
            {
                return Unauthorized(new { message = "Usuario no activado." });
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDto.CredencialPassword, usuario.Credenciales.CredencialPassword);

            if (!isPasswordCorrect)
            {
                return Unauthorized(new { message = "Correo o contraseña incorrectos." });
            }

            // Ocultar la contraseña en la respuesta
            usuario.Credenciales.CredencialPassword = "[PROTECTED]";

            return Ok("Login exitoso.");
        }


        [HttpGet("GetUsuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosWithCredenciales()
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Credenciales) // incluye la propiedad de navegacion credenciales
                .ToListAsync();
            return usuarios;
        }

        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UsuarioResponseDto>> GetUserByEmail([FromQuery] string email)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Credenciales)
                .FirstOrDefaultAsync(u => u.Credenciales.CredencialCorreo == email);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            // Mapear a DTO para evitar exponer datos sensibles
            var usuarioDto = _mapper.Map<UsuarioResponseDto>(usuario);

            return Ok(usuarioDto);
        }



        //Activar cuenta

        [HttpPost("Activate")]
        public async Task<ActionResult> ActivateAccount([FromBody] ActivationDto activationDto)
        {
            Usuario usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.UsuarioTokenActivacion == activationDto.Token);

            if (usuario == null)
            {
                return BadRequest(new { message = "Token de activación inválido." });
            }

            if (usuario.UsuarioTokenVence < DateTime.UtcNow)
            {
                return BadRequest(new { message = "El token de activación ha expirado." });
            }

            usuario.UsuarioActivo = true;
            usuario.UsuarioTokenActivacion = null;
            usuario.UsuarioTokenVence = DateTime.MinValue;

            await _context.SaveChangesAsync();

            return Ok("Cuenta activada exitosamente.");
        }


        //Recuperar Password

       


    }
    
}
