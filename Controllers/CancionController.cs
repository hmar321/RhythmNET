using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RhythmBack.Data.Repository;
using RhythmBack.Model.Context;
using RhythmBack.Model.DTO;
using RhythmBack.Model.Models;

namespace RhythmBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RhythmDBContext _dbContext;
        private readonly Repository<Cancion> _repository;
        private readonly CancionRepository _cancionRepository;
        public CancionController(RhythmDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _repository = new Repository<Cancion>(_dbContext);
            _cancionRepository = new CancionRepository(_dbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cancion[]>>> GetAll()
        {
            var canciones = await _repository.GetAllAsync();
            if (canciones == null)
                return NotFound();
            var cancionesDto = _mapper.Map<IEnumerable<CancionDTO>>(canciones);
            foreach (var cancionDto in cancionesDto)
            {
                cancionDto.Portada = await _cancionRepository.GetPortadaEstrenoAsync(cancionDto.Id);
                cancionDto.Artistas = await _cancionRepository.GetArtistasConcatAsync(cancionDto.Id);
            }
            return Ok(cancionesDto);
        }

        [HttpGet("Exitos")]
        public async Task<ActionResult<IEnumerable<Cancion[]>>> GetExitos()
        {
            var canciones = await _cancionRepository.GetExitosAsync();
            if (canciones == null)
                return NotFound();
            var cancionesDto = _mapper.Map<IEnumerable<CancionDTO>>(canciones);
            foreach (var cancionDto in cancionesDto)
            {
                cancionDto.Portada = await _cancionRepository.GetPortadaEstrenoAsync(cancionDto.Id);
                cancionDto.Artistas = await _cancionRepository.GetArtistasConcatAsync(cancionDto.Id);
                cancionDto.Estreno = await _cancionRepository.GetEstrenoAsync(cancionDto.Id);
            }
            return Ok(cancionesDto);
        }

            [HttpGet("{id}")]
        public async Task<ActionResult<Cancion>> GetById(int id)
        {
            var cancion = await _repository.GetByIdAsync(id);
            if (cancion == null)
                return NotFound();
            var cancionDto = _mapper.Map<CancionDTO>(cancion);
            cancionDto.Portada = await _cancionRepository.GetPortadaEstrenoAsync(cancionDto.Id);
            cancionDto.Artistas = await _cancionRepository.GetArtistasConcatAsync(cancionDto.Id);
            cancionDto.Estreno = await _cancionRepository.GetEstrenoAsync(cancionDto.Id);
            return Ok(cancionDto);
        }
        [HttpPost]
        public async Task<ActionResult<Cancion>> Create(Cancion cancion)
        {
            await _repository.AddAsync(cancion);
            return CreatedAtAction(nameof(GetById), new { id = cancion.Id }, cancion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cancion cancion)
        {
            if (id != cancion.Id)
                return BadRequest();
            await _repository.UpdateAsync(cancion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cancion = await _repository.GetByIdAsync(id);
            if (cancion == null)
                return NotFound();
            await _repository.DeleteAsync(cancion);
            return NoContent();
        }
        [HttpGet("Buscador")]
        public async Task<ActionResult<IEnumerable<ArtistaDTO>>> GetByTitulo([FromQuery] string termino)
        {
            var canciones = await _cancionRepository.GetByTituloAsync(termino);
            if (canciones == null)
                return NotFound();
            var cancionesDto = _mapper.Map<IEnumerable<CancionDTO>>(canciones);
            foreach (var cancionDto in cancionesDto)
            {
                cancionDto.Portada = await _cancionRepository.GetPortadaEstrenoAsync(cancionDto.Id);
                cancionDto.Artistas = await _cancionRepository.GetArtistasConcatAsync(cancionDto.Id);
                cancionDto.Estreno = await _cancionRepository.GetEstrenoAsync(cancionDto.Id);
            }
            return Ok(cancionesDto);
        }
    }
}
