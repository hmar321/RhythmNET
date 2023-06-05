using Microsoft.AspNetCore.Mvc;
using RhythmBack.Data.Repository;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;
using AutoMapper;
using RhythmBack.Model.DTO;

namespace RhythmBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RhythmDBContext _dbContext;
        private readonly Repository<Usuario> _repository;
        private readonly UsuarioRepository _usuarioRepository;
        public UsuarioController(RhythmDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _repository = new Repository<Usuario>(_dbContext);
            _usuarioRepository = new UsuarioRepository(_dbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario[]>>> GetAll()
        {
            var usuarios = await _repository.GetAllAsync(u => u.Albums!, u => u.Artistas!, u => u.Listas!, u => u.ListasCreadas!, u => u.Rol!);
            if (usuarios == null)
                return NotFound();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _repository.GetByIdAsync(id, u => u.Albums!, u => u.Artistas!, u => u.Listas!, u => u.ListasCreadas!, u => u.Rol!);
            if (usuario == null)
                return NotFound();
            var usuarioDto = _mapper.Map<UsuarioDTO>(usuario);
            return Ok(usuarioDto);
        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.Rol = _dbContext.Roles!.FirstOrDefault(r => r.Nombre == "Usuario");
            usuario.ListasCreadas = new List<Lista> { new Lista { Titulo = "Favoritos", Portada = "Favoritos.png", Visitas = 0 } };
            await _repository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UsuarioDTO usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest();
            var usuarioDb = await _repository.GetByIdAsync(id);
            if (usuarioDb == null)
                return NotFound();
            var usuario = _mapper.Map(usuarioDto, usuarioDb);
            await _repository.UpdateAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();
            await _repository.DeleteAsync(usuario);
            return NoContent();
        }

        [HttpGet("Login")]
        public async Task<ActionResult<Usuario>> GetByEmailAndPassword([FromQuery] string email, [FromQuery] string password)
        {
            var usuario = await _usuarioRepository.GetByEmailAndPassord(email, password);
            if (usuario == null)
                return NotFound();
            if (!usuario.Password!.Equals(password))
                return NotFound();
            return Ok(usuario);
        }

        [HttpDelete("QuitarLista")]
        public async Task<IActionResult> RemoveListaFromListasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var usuarioDb = await _repository.GetByIdAsync(id, u => u.Listas!);
            if (usuarioDb == null)
                return NotFound();
            var lista = (from li in usuarioDb.Listas where li.Id == idElemento select li).FirstOrDefault();
            if (lista == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Lista>(id, idElemento, o => o.Listas!, e => e.Usuarios!);
            return NoContent();
        }

        [HttpDelete("QuitarAlbum")]
        public async Task<IActionResult> RemoveAlbumFromAlbumsCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var usuarioDb = await _repository.GetByIdAsync(id, u => u.Albums!);
            if (usuarioDb == null)
                return NotFound();
            var album = (from al in usuarioDb.Albums where al.Id == idElemento select al).FirstOrDefault();
            if (album == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Album>(id, idElemento, o => o.Albums!, e => e.Usuarios!);
            return NoContent();
        }

        [HttpDelete("QuitarArtista")]
        public async Task<IActionResult> RemoveArtistaFromArtistasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var usuarioDb = await _repository.GetByIdAsync(id, u => u.Artistas!);
            if (usuarioDb == null)
                return NotFound();
            var artista = (from ar in usuarioDb.Artistas where ar.Id == idElemento select ar).FirstOrDefault();
            if (artista == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Artista>(id, idElemento, o => o.Artistas!, e => e.Usuarios!);
            return NoContent();
        }

        [HttpPut("AddLista")]
        public async Task<IActionResult> AddListaIntoListasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var usuarioDb = await _repository.GetByIdAsync(id, u => u.Listas!);
            if (usuarioDb == null)
                return NotFound();
            var lista = (from li in usuarioDb.Listas where li.Id == idElemento select li).FirstOrDefault();
            if (lista != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Lista>(id, idElemento, o => o.Listas!, e => e.Usuarios!);
            return NoContent();
        }
        [HttpPut("AddAlbum")]
        public async Task<IActionResult> AddAlbumIntoAlbumsCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var usuarioDb = await _repository.GetByIdAsync(id, u => u.Albums!);
            if (usuarioDb == null)
                return NotFound();
            var album = (from al in usuarioDb.Albums where al.Id == idElemento select al).FirstOrDefault();
            if (album != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Album>(id, idElemento, o => o.Albums!, e => e.Usuarios!);
            return NoContent();
        }
        [HttpPut("AddArtista")]
        public async Task<IActionResult> AddArtistaIntoArtistasCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var usuarioDb = await _repository.GetByIdAsync(id, u => u.Artistas!);
            if (usuarioDb == null)
                return NotFound();
            var artista = (from ar in usuarioDb.Artistas where ar.Id == idElemento select ar).FirstOrDefault();
            if (artista != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Artista>(id, idElemento, o => o.Artistas!, e => e.Usuarios!);
            return NoContent();
        }
    }
}
