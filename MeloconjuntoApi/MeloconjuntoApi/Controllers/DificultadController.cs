using AutoMapper;
using MeloconjuntoApi.Data.Context;
using MeloconjuntoApi.Dtos.Dificultades;
using MeloconjuntoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeloconjuntoApi.Controllers
{
    [Route("api/Dificultad")]
    [ApiController]
    public class DificultadController : ControllerBase
    {
        private readonly ILogger<DificultadController> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DificultadController(ILogger<DificultadController> logger, DataContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("CrearDificultad")]
        public async Task<ActionResult<Dificultad>> CrearDificultad([FromBody] DificultadDto dificultadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dificultad existingDificultad = await _context.Dificultades
            .FirstOrDefaultAsync(d => d.DificultadNombre == dificultadDto.DificultadNombre);

            if (existingDificultad != null)
            {
                return BadRequest(new { message = "Ya existe una dificultad con este nombre." });
            }

            Dificultad dificultad = _mapper.Map<Dificultad>(dificultadDto);

            _context.Dificultades.Add(dificultad);

            await _context.SaveChangesAsync();
            return Ok(dificultad);
        }

        [HttpGet("GetDificultades")]
        public async Task<ActionResult<IEnumerable<GetDificultadDto>>> GetDificultades()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dificultades = await _context.Dificultades.ToListAsync();
            if (!dificultades.Any())
            {
                return NotFound(new { message = "No existen dificultades" });
            }

            return Ok(_mapper.Map<IEnumerable<GetDificultadDto>>(dificultades));
        }

        [HttpGet("GetDificultad/{id}")]
        public async Task<ActionResult<Dificultad>> GetDificultad(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Dificultad dificultad = await _context.Dificultades
                .FirstOrDefaultAsync(d => d.DificultadId == id);

            return dificultad;
        }

        [HttpPut("ActualizarDificultad/{id}")]
        public async Task<ActionResult<Dificultad>> ActualizarDificultad(int id, [FromBody] DificultadDto dificultadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Dificultad existingDificultad = await _context.Dificultades
                .FirstOrDefaultAsync(d => d.DificultadId == id);

            if (existingDificultad == null)
            {
                return BadRequest("No existe una dificultad con este id.");
            }



            _mapper.Map(dificultadDto, existingDificultad);

            await _context.SaveChangesAsync();

            return Ok(existingDificultad);
        }

        [HttpDelete("EliminarDificultad/{id}")]
        public async Task<ActionResult> EliminarDificultad(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                Dificultad dificultad = await _context.Dificultades
                .FirstOrDefaultAsync(d => d.DificultadId == id);

            if (dificultad == null)
            {
              return NotFound(); 
            }

            _context.Dificultades.Remove(dificultad);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
