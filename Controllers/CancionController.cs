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
        [HttpGet("BuscadorForLista")]
        public async Task<ActionResult<IEnumerable<ArtistaDTO>>> GetByTituloForLista([FromQuery] string termino, [FromQuery]int idLista)
        {
            var canciones = await _cancionRepository.GetByTituloAsyncForLista(termino,idLista);
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
        [HttpPut("AddLista")]
        public async Task<IActionResult> AddListaIntoListasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, u => u.Listas!);
            if (cancionDb == null)
                return NotFound();
            var lista = (from li in cancionDb.Listas where li.Id == idElemento select li).FirstOrDefault();
            if (lista != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Lista>(id, idElemento, o => o.Listas!, e => e.Canciones!);
            return NoContent();
        }

        [HttpDelete("QuitarLista")]
        public async Task<IActionResult> RemoveListaFromListasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, li => li.Listas!);
            if (cancionDb == null)
                return NotFound();
            var lista = (from ca in cancionDb.Listas where ca.Id == idElemento select ca).FirstOrDefault();
            if (lista == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Lista>(id, idElemento, o => o.Listas!, e => e.Canciones!);
            return NoContent();
        }

        [HttpPut("AddAlbum")]
        public async Task<IActionResult> AddAlbumIntoAlbumsCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, u => u.Albums!);
            if (cancionDb == null)
                return NotFound();
            var album = (from al in cancionDb.Albums where al.Id == idElemento select al).FirstOrDefault();
            if (album != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Album>(id, idElemento, o => o.Albums!, e => e.Canciones!);
            return NoContent();
        }

        [HttpDelete("QuitarAlbum")]
        public async Task<IActionResult> RemoveAlbumFromAlbumsCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, li => li.Albums!);
            if (cancionDb == null)
                return NotFound();
            var album = (from al in cancionDb.Albums where al.Id == idElemento select al).FirstOrDefault();
            if (album == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Album>(id, idElemento, o => o.Albums!, e => e.Canciones!);
            return NoContent();
        }

        [HttpPut("AddArtista")]
        public async Task<IActionResult> AddArtistaIntoArtistasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, u => u.Artistas!);
            if (cancionDb == null)
                return NotFound();
            var artista = (from ar in cancionDb.Artistas where ar.Id == idElemento select ar).FirstOrDefault();
            if (artista != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Artista>(id, idElemento, o => o.Artistas!, e => e.Canciones!);
            return NoContent();
        }

        [HttpDelete("QuitarArtista")]
        public async Task<IActionResult> RemoveArtistaFromArtistasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, li => li.Artistas!);
            if (cancionDb == null)
                return NotFound();
            var artista = (from ca in cancionDb.Artistas where ca.Id == idElemento select ca).FirstOrDefault();
            if (artista == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Artista>(id, idElemento, o => o.Artistas!, e => e.Canciones!);
            return NoContent();
        }

        [HttpPut("AddGenero")]
        public async Task<IActionResult> AddGeneroIntoGenerosCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, u => u.Generos!);
            if (cancionDb == null)
                return NotFound();
            var genero = (from ge in cancionDb.Generos where ge.Id == idElemento select ge).FirstOrDefault();
            if (genero != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Genero>(id, idElemento, o => o.Generos!, e => e.Canciones!);
            return NoContent();
        }

        [HttpDelete("QuitarGenero")]
        public async Task<IActionResult> RemoveGeneroFromGenerosCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var cancionDb = await _repository.GetByIdAsync(id, li => li.Generos!);
            if (cancionDb == null)
                return NotFound();
            var genero = (from ca in cancionDb.Generos where ca.Id == idElemento select ca).FirstOrDefault();
            if (genero == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Genero>(id, idElemento, o => o.Generos!, e => e.Canciones!);
            return NoContent();
        }
    }
}
