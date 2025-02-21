using AutoMapper;
using MeloconjuntoApi.Data.Context;
using MeloconjuntoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using MeloconjuntoApi.Dtos.Mapas;

namespace MeloconjuntoApi.Controllers
{
    [Route("api/Mapa")]
    [ApiController]
    public class MapaController : ControllerBase
    {
        private readonly ILogger<MapaController> _logger;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MapaController(ILogger<MapaController> logger, DataContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("CrearMapa")]
        public async Task<ActionResult<Mapa>> CrearMapa([FromBody] MapaDto mapaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapa existingMapa = await _context.Mapas
                .FirstOrDefaultAsync(m => m.MapaNombre == mapaDto.MapaNombre);

            if (existingMapa != null)
            {
                return BadRequest(new { message = "Ya existe un mapa con ese nombre" });
            }

            Mapa mapa = _mapper.Map<Mapa>(mapaDto);

            _context.Mapas.Add(mapa);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Mapa creado exitosamente" });

        }

        [HttpGet("ObtenerMapas")]
        public async Task<ActionResult<IEnumerable<GetMapaDto>>> GetMapas()
        {
            var mapas = await _context.Mapas
                .Include(m => m.Dificultad)
                .ToListAsync();

            var mapasDto = _mapper.Map<List<GetMapaDto>>(mapas);

            return Ok(mapasDto);
        }
    }
}
