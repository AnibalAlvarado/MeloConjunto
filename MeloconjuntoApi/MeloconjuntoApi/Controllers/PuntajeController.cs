using AutoMapper;
using MeloconjuntoApi.Data.Context;
using MeloconjuntoApi.Dtos.Puntajes;
using MeloconjuntoApi.Models;
using MeloconjuntoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 namespace MeloconjuntoApi.Controllers
 {
        [Route("api/Puntaje")]
        [ApiController]
        public class PuntajeController : ControllerBase
        {
            private readonly ILogger<PuntajeController> _logger;
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly PuntajeService _puntajeService;

        public PuntajeController(ILogger<PuntajeController> logger, DataContext context, IMapper mapper, PuntajeService puntajeService)
            {
                _logger = logger;
                _context = context;
                _mapper = mapper;
                _puntajeService = puntajeService;
            }

           [HttpPost("RegistrarPuntaje")]
            public async Task<ActionResult<Puntaje>> RegistrarPuntaje([FromBody] PuntajeDto puntajeDto)
            {
              if (!ModelState.IsValid)
                {
                return BadRequest(ModelState);
                }

                // Validar que el Usuario y Nivel existan
                bool usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.UsuarioId == puntajeDto.UsuarioId);

                bool nivelExiste = await _context.Niveles
                .AnyAsync(n => n.NivelId == puntajeDto.NivelId);

                if (!usuarioExiste)
                {
                    return BadRequest(new { message = "El usuario no existe." });
                }

                if (!nivelExiste)
                {
                    return BadRequest(new { message = "El nivel no existe." });
                }

                // Buscar si ya existe un puntaje para este usuario en este nivel
                Puntaje existingPuntaje = await _context.Puntajes
                    .FirstOrDefaultAsync(p => p.UsuarioId == puntajeDto.UsuarioId
                                           && p.NivelId == puntajeDto.NivelId);

                if (existingPuntaje != null)
                {
                    // Si el nuevo puntaje es mejor que el existente, actualizamos
                    if (_puntajeService.EsPuntajeMejor(puntajeDto, existingPuntaje))
                    {
                        _mapper.Map(puntajeDto, existingPuntaje);
                    }
                    else
                    {
                        // Si no es mejor, simplemente devolvemos el existente
                        return Ok(existingPuntaje);
                    }
                }
                else
                {
                    // Si no existe, creamos un nuevo puntaje
                    existingPuntaje = _mapper.Map<Puntaje>(puntajeDto);
                    _context.Puntajes.Add(existingPuntaje);
                }

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Devolver el puntaje
                return Ok(existingPuntaje);
            }

        [HttpGet("GetPuntajes")]
        public async Task<ActionResult<IEnumerable<Puntaje>>> GetPuntajes()
        {
            var puntajes = await _context.Puntajes
                .ToListAsync();
            return puntajes;
        }
    }
       
}
