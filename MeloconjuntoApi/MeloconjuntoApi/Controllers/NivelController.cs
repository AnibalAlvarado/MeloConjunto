using AutoMapper;
using MeloconjuntoApi.Data.Context;
using MeloconjuntoApi.Dtos.Niveles;
using MeloconjuntoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MeloconjuntoApi.Controllers
{
    [Route("api/Nivel")]
    [ApiController]
    public class NivelController : ControllerBase
    {
        private readonly ILogger<NivelController> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public NivelController(ILogger<NivelController> logger, DataContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("CrearNivel")]
        public async Task<ActionResult<NivelResponseDto>> CrearNivel(NivelDto nivelDto)
        {
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                Nivel existingNivel = await _context.Niveles
               .Include(n => n.Mapa)
               .ThenInclude(m => m.Dificultad)
               .FirstOrDefaultAsync(n => n.NivelName == nivelDto.NivelName && n.Mapa.DificultadId == nivelDto.MapaId);

                if (existingNivel != null)
                {
                    return BadRequest($"Ya existe un nivel con el nombre '{nivelDto.NivelName}' en la misma dificultad.");
                }
                // Mapear y crear el nivel
                Nivel nivel = _mapper.Map<Nivel>(nivelDto);
                _context.Niveles.Add(nivel);
                await _context.SaveChangesAsync();

                // Mapear y crear la pregunta
                Pregunta pregunta = _mapper.Map<Pregunta>(nivelDto.Pregunta);
                pregunta.NivelId = nivel.NivelId;
                _context.Preguntas.Add(pregunta);
                await _context.SaveChangesAsync();

                // Mapear y crear las respuestas
                List<Respuesta> respuestas = _mapper.Map<List<Respuesta>>(nivelDto.Pregunta.Respuestas);
                foreach (Respuesta respuesta in respuestas)
                {
                    respuesta.PreguntaId = pregunta.PreguntaId;
                    _context.Respuestas.Add(respuesta);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Obtener el nivel completo con sus relaciones
                Nivel nivelCreado = await _context.Niveles
                    .Include(n => n.Pregunta)
                    .ThenInclude(p => p.Respuestas)
                    .FirstAsync(n => n.NivelId == nivel.NivelId);

                // Mapear a DTO de respuesta
                NivelResponseDto response = _mapper.Map<NivelResponseDto>(nivelCreado);

                return Ok("Nivel creado exitosamente");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error al crear el nivel {ex.Message}");
            }
        }

        [HttpGet("ObtenerNivel/{id}")]
        public async Task<ActionResult<NivelResponseDto>> GetNivel(int id)
        {
            Nivel nivel = await _context.Niveles
                .Include(n => n.Pregunta)
                .ThenInclude(p => p.Respuestas)
                .FirstOrDefaultAsync(n => n.NivelId == id);

            if (nivel == null)
            {
                return NotFound();
            }

            NivelResponseDto response = _mapper.Map<NivelResponseDto>(nivel);
            return Ok(response);
        }

       

        [HttpGet("ObtenerNiveles")]
        public async Task<ActionResult<IEnumerable<NivelResponseDto>>> GetNiveles()
        {
            List<Nivel> niveles = await _context.Niveles
                .Include(n => n.Pregunta)
                .ThenInclude(p => p.Respuestas)
                .ToListAsync();

            if (!niveles.Any())
            {
                return NoContent();
            }

            List<NivelResponseDto> response = _mapper.Map<List<NivelResponseDto>>(niveles);
            return Ok(response);
        }

        [HttpGet("ObtenerNivelesDificultad/{id}")]
        public async Task<ActionResult<IEnumerable<GetNivelDificultadDto>>> ObtenerNivelesPorDificultad(int id)
        {
            List<Nivel> niveles = await _context.Niveles
                .Include(n => n.Mapa)
                .ThenInclude(m => m.Dificultad)
                .Where(n => n.Mapa.DificultadId == id)
                .ToListAsync();

           IEnumerable<GetNivelDificultadDto> nivelDificultadDtos = _mapper.Map<IEnumerable<GetNivelDificultadDto>>(niveles);

            return Ok(nivelDificultadDtos);
        }

        [HttpGet("ObtenerRespuestas/{id}")]
        public async Task<ActionResult<List<RespuestaResponseDto>>> GetRespuestasNivel(int id)
        {
            // Buscar el nivel y cargar sus preguntas y respuestas
            Nivel nivel = await _context.Niveles
                .Include(n => n.Pregunta)
                    .ThenInclude(p => p.Respuestas)
                .FirstOrDefaultAsync(n => n.NivelId == id);

            // Verificar si el nivel existe
            if (nivel == null)
            {
                return NotFound($"No se encontró un nivel con ID {id}");
            }

            // Verificar si el nivel tiene una pregunta
            if (nivel.Pregunta == null)
            {
                return NotFound($"El nivel con ID {id} no tiene una pregunta asociada");
            }

            // Mapear las respuestas de la pregunta a DTOs
            List<RespuestaResponseDto> respuestasDto = _mapper.Map<List<RespuestaResponseDto>>(nivel.Pregunta.Respuestas);

            return Ok(respuestasDto);
        }
    }
}
