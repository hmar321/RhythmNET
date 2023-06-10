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
    public class AlbumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RhythmDBContext _dbContext;
        private readonly Repository<Album> _repository;
        private readonly AlbumRepository _albumRepository;
        public AlbumController(RhythmDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _repository = new Repository<Album>(_dbContext);
            _albumRepository = new AlbumRepository(_dbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album[]>>> GetAll()
        {
            var albums = await _repository.GetAllAsync();
            if (albums == null)
                return NotFound();
            var albumsDto = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
            foreach (var albumDto in albumsDto)
            {
                albumDto.ArtistasCadena = await _albumRepository.GetArtistasConcatByAlbumIdAsync(albumDto.Id);
            }
            return Ok(albumsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetById(int id)
        {
            var album = await _repository.GetByIdAsync(id, al => al.Canciones!);
            var albumDto = _mapper.Map<AlbumDTO>(album);
            foreach (var cancionDto in albumDto.Canciones!)
            {
                cancionDto.Portada = albumDto.Portada;
                cancionDto.ArtistasCadena = await _albumRepository.GetArtistasConcatByCancionIdAsync(cancionDto.Id);
            }
            var artistas = await _albumRepository.GetArtistasConcatByAlbumIdAsync(album.Id);
            albumDto.ArtistasCadena = await _albumRepository.GetArtistasConcatByAlbumIdAsync(album.Id);
            if (albumDto == null)
            {
                return NotFound();
            }
            return Ok(albumDto);
        }
        [HttpPost]
        public async Task<ActionResult<Album>> Create(AlbumDTO albumDto)
        {
            var album=_mapper.Map<Album>(albumDto);
            await _repository.AddAsync(album);
            return CreatedAtAction(nameof(GetById), new { id = album.Id }, album);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AlbumDTO albumDto)
        {
            var album = _mapper.Map<Album>(albumDto);
            if (id != album.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(album);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _repository.GetByIdAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(album);
            return NoContent();
        }

        [HttpGet("Buscador")]
        public async Task<ActionResult<IEnumerable<Album[]>>> GetByTitulo([FromQuery] string termino)
        {
            var albums = await _albumRepository.GetByTituloAsync(termino);
            if (albums == null)
                return NotFound();
            var albumsDto = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
            foreach (var albumDto in albumsDto)
            {
                albumDto.ArtistasCadena = await _albumRepository.GetArtistasConcatByAlbumIdAsync(albumDto.Id);
            }
            return Ok(albumsDto);
        }
        [HttpGet("Exitos")]
        public async Task<ActionResult<IEnumerable<Album[]>>> GetExitos()
        {
            var albums = await _albumRepository.GetExitosAsync();
            if (albums == null)
                return NotFound();
            var albumsDto = _mapper.Map<IEnumerable<AlbumDTO>>(albums);
            foreach (var albumDto in albumsDto)
            {
                albumDto.ArtistasCadena = await _albumRepository.GetArtistasConcatByAlbumIdAsync(albumDto.Id);
            }
            return Ok(albumsDto);
        }
    }
}
