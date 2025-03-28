﻿using AutoMapper;
using MeloconjuntoApi.Data.Context;
using MeloconjuntoApi.Dtos.Password;
using MeloconjuntoApi.Models;
using MeloconjuntoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeloconjuntoApi.Controllers
{
    [Route("api/Password")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly ILogger<PasswordController> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;

        public PasswordController(ILogger<PasswordController> logger, DataContext context, IMapper mapper, EmailService emailService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpPost("RecuperarPassword")]
        public async Task<ActionResult<RecuperaPassword>> recuperarPasswords([FromBody] RecuperaPasswordDto recuperaPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Usuario usuario = await _context.Usuarios
            .Include(u => u.Credenciales)
            .FirstOrDefaultAsync(u => u.Credenciales.CredencialCorreo == recuperaPasswordDto.RecuperaPasswordCorreo);

            if (usuario == null)
            {
                return BadRequest(new { message = "No existe una cuenta vinculada a este correo" });
            }

            List<RecuperaPassword> solicitudesActivas = await _context.RecuperarPasswords
           .Where(r => r.UsuarioId == usuario.UsuarioId &&
                r.RecuperaPasswordFechaLimite > DateTime.UtcNow)
           .ToListAsync();

            if (solicitudesActivas.Any())
            {
                _context.RecuperarPasswords.RemoveRange(solicitudesActivas);
                await _context.SaveChangesAsync();
            }


            RecuperaPassword recuPassword = _mapper.Map<RecuperaPassword>(recuperaPasswordDto);
            recuPassword.UsuarioId = usuario.UsuarioId;
            string subjectEmail = "Cambia tu contraseña";
            string bodyEmail = $"""
            Hola {usuario.UsuarioNombre},

            Has solicitado cambiar tu contraseña. Usa el siguiente código para completar el proceso:

            {recuPassword.RecuperaPasswordToken}

            Este código expirará en 24 horas por razones de seguridad.

            Si no solicitaste este cambio, por favor ignora este correo o contacta a soporte.

            Saludos,
            El equipo de MeloConjunto
            """;

            _context.RecuperarPasswords.Add(recuPassword);
            await _context.SaveChangesAsync();
            await _emailService.EnviarEmailAsync(recuperaPasswordDto.RecuperaPasswordCorreo, subjectEmail, bodyEmail);
            return Ok("Token de recuperacion enviado exitosamente");
        }


        [HttpGet("GetRecuperaPassword")]
        public async Task<ActionResult<IEnumerable<RecuperaPassword>>> GetRecuperaPassword()
        {
            var recuperaRegistros = await _context.RecuperarPasswords
                .ToListAsync();
            return recuperaRegistros;
        }

        [HttpPost("recuperarToken")]
        public async Task<ActionResult> RecuperarToken([FromBody] RecuperaTokenDto recuperaTokenDto)
        {
            RecuperaPassword recuperaPassword = await _context.RecuperarPasswords
                .FirstOrDefaultAsync(rp => rp.RecuperaPasswordToken == recuperaTokenDto.Token && rp.RecuperaPasswordFechaLimite > DateTime.UtcNow);

            if (recuperaPassword == null)
            {
                return BadRequest(new { message = "Token de recuperacion de contraseña inválido o expirado." });
            }
            await _context.SaveChangesAsync();

            return Ok("Token de recuperacion de contraseña valido.");

        }

        [HttpPut("CambiarPassword/{token}")]
        public async Task<IActionResult> CambiarPassword(string token, [FromBody] CambioPasswordDto cambioPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           

            // Buscar la solicitud de recuperación de contraseña válida
            var recuperacionPassword = await _context.RecuperarPasswords
                .Include(r => r.Usuario)
                    .ThenInclude(u => u.Credenciales)
                .FirstOrDefaultAsync(r =>
                    r.RecuperaPasswordToken == token &&
                    r.RecuperaPasswordFechaLimite > DateTime.UtcNow);

            if (recuperacionPassword == null)
            {
                return BadRequest(new { message = "Token inválido o expirado" });
            }

            if (cambioPasswordDto.Password != cambioPasswordDto.ConfirmPassword)
            {
                return BadRequest(new { message = "Las contraseñas no coinciden" });
            }
            // Actualizar la contraseña
            recuperacionPassword.Usuario.Credenciales.CredencialPassword = cambioPasswordDto.Password;

            // Marcar el token como usado (opcional: puedes eliminarlo o marcarlo como usado)
            _context.RecuperarPasswords.Remove(recuperacionPassword);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Contraseña actualizada exitosamente" });
        }
    }
}
