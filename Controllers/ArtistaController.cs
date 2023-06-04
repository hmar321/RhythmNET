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
    public class ArtistaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RhythmDBContext _dbContext;
        private readonly Repository<Artista> _repository;
        private readonly ArtistaRepository _artistaRepository;
        public ArtistaController(RhythmDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _repository = new Repository<Artista>(_dbContext);
            _artistaRepository = new ArtistaRepository(_dbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artista[]>>> GetAll()
        {
            var artistas = await _repository.GetAllAsync();
            if (artistas == null)
                return NotFound();
            var artistasDto = _mapper.Map<IEnumerable<ArtistaDTO>>(artistas);
            foreach (var artistaDto in artistasDto)
            {
                var albums = await _artistaRepository.GetAlbumsByArtistaId(artistaDto.Id);
                var albumsDto = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
                artistaDto.Albums = albumsDto.OrderBy(al => al.Artistas).ToList();
            }
            return Ok(artistasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artista>> GetById(int id)
        {
            var artista = await _repository.GetByIdAsync(id);
            if (artista == null)
                return NotFound();
            var artistaDto = _mapper.Map<ArtistaDTO>(artista);
            var albums = await _artistaRepository.GetAlbumsByArtistaId(artista.Id);
            var albumsDto = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
            foreach (var albumDto in albumsDto)
            {
                albumDto.Artistas = await _artistaRepository.GetArtistasTituloConcatByAlbumIdAsync(albumDto.Id);
                var canciones = await _artistaRepository.GetCancionesByAlbumIdAsync(albumDto.Id);
                var cancionesDto = _mapper.Map<IEnumerable<CancionDTO>>(canciones);
                albumDto.Canciones = cancionesDto.ToList();
            }
            artistaDto.Albums = albumsDto.ToList();
            return Ok(artistaDto);
        }
        [HttpPost]
        public async Task<ActionResult<Artista>> Create(Artista artista)
        {
            await _repository.AddAsync(artista);
            return CreatedAtAction(nameof(GetById), new { id = artista.Id }, artista);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Artista artista)
        {
            if (id != artista.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(artista);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var artista = await _repository.GetByIdAsync(id);
            if (artista == null)
                return NotFound();

            await _repository.DeleteAsync(artista);
            return NoContent();
        }

        [HttpGet("Buscador")]
        public async Task<ActionResult<IEnumerable<ArtistaDTO>>> GetByTitulo([FromQuery] string termino)
        {
            var artistas = await _artistaRepository.GetByTituloAsync(termino);
            if (artistas == null)
                return NotFound();
            var artistasDto = _mapper.Map<IEnumerable<ArtistaDTO>>(artistas);
            foreach (var artistaDto in artistasDto)
            {
                var albums = await _artistaRepository.GetAlbumsByArtistaId(artistaDto.Id);
                var albumsDto = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
                artistaDto.Albums = albumsDto.ToList();
            }
            return Ok(artistasDto);
        }

        [HttpGet("Exitos")]
        public async Task<ActionResult<IEnumerable<Artista[]>>> GetExitos()
        {
            var artistas = await _artistaRepository.GetExitosAsync();
            if (artistas == null)
                return NotFound();
            var artistasDto = _mapper.Map<IEnumerable<ArtistaDTO>>(artistas);
            foreach (var artistaDto in artistasDto)
            {
                var albums = await _artistaRepository.GetAlbumsByArtistaId(artistaDto.Id);
                var albumsDto = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
                artistaDto.Albums = albumsDto.OrderBy(al => al.Artistas).ToList();
            }
            return Ok(artistasDto);
        }

    }
}
