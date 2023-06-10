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
    public class ListaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RhythmDBContext _dbContext;
        private readonly Repository<Lista> _repository;
        private readonly ListaRepository _listaRepository;
        public ListaController(RhythmDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _repository = new Repository<Lista>(_dbContext);
            _listaRepository = new ListaRepository(_dbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lista[]>>> GetAll()
        {
            var listas = await _repository.GetAllAsync(li => li.Canciones!, li => li.Creador!);
            if (listas == null)
                return NotFound();
            var listasDto = _mapper.Map<IEnumerable<ListaDTO>>(listas);
            foreach (var lista in listas)
            {
                var listaDto = listasDto.Single(a => a.Id == lista.Id);
                listaDto.CreadorNick = lista.Creador!.Nick;
            }
            return Ok(listasDto);
        }

        [HttpGet("Exitos")]
        public async Task<ActionResult<IEnumerable<Lista[]>>> GetExitos([FromQuery] int id)
        {
            var listas = await _listaRepository.GetExitosAsync(id);
            if (listas == null)
                return NotFound();
            var listasDto = _mapper.Map<IEnumerable<ListaDTO>>(listas);
            foreach (var lista in listas)
            {
                var listaDto = listasDto.Single(a => a.Id == lista.Id);
                listaDto.CreadorNick = lista.Creador!.Nick;
            }
            return Ok(listasDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lista>> GetById(int id)
        {
            var lista = await _repository.GetByIdAsync(id, li => li.Canciones!, li => li.Creador!);
            if (lista == null)
                return NotFound();
            var listaDto = _mapper.Map<ListaDTO>(lista);
            listaDto.CreadorNick = lista.Creador!.Nick;
            foreach (var cancionDto in listaDto.Canciones!)
            {
                cancionDto.ArtistasCadena = await _listaRepository.GetArtistasConcatByCancionIdAsync(cancionDto.Id);
            }
            return Ok(listaDto);
        }
        [HttpPost]
        public async Task<ActionResult<Lista>> Create(ListaDTO listaDto)
        {
            var lista = _mapper.Map<Lista>(listaDto);
            await _repository.AddAsync(lista);
            return CreatedAtAction(nameof(GetById), new { id = lista.Id }, lista);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ListaDTO listaDto)
        {
            var lista = _mapper.Map<Lista>(listaDto);
            if (id != lista.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(lista);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lista = await _repository.GetByIdAsync(id);
            if (lista == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(lista);
            return NoContent();
        }

        [HttpGet("Buscador")]
        public async Task<ActionResult<IEnumerable<ListaDTO>>> GetByTitulo([FromQuery] string termino, [FromQuery] int id)
        {
            var listas = await _listaRepository.GetByTituloAsync(termino, id);
            if (listas == null)
                return NotFound();
            var listasDto = _mapper.Map<IEnumerable<ListaDTO>>(listas);
            foreach (var lista in listas)
            {
                var listaDto = listasDto.Single(a => a.Id == lista.Id);
                listaDto.CreadorNick = lista.Creador!.Nick;
            }
            return Ok(listasDto);
        }

        [HttpDelete("QuitarCancion")]
        public async Task<IActionResult> RemoveCancionFromCancionsCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var listaDb = await _repository.GetByIdAsync(id, li => li.Canciones!);
            if (listaDb == null)
                return NotFound();
            var cancion = (from ca in listaDb.Canciones where ca.Id == idElemento select ca).FirstOrDefault();
            if (cancion == null)
                return NotFound();
            await _repository.RemoveElementFromColeccionAsync<Cancion>(id, idElemento, o => o.Canciones!, e => e.Listas!);
            return NoContent();
        }

        [HttpPut("AddCancion")]
        public async Task<IActionResult> AddCancionIntoCancionesCollectionAsync([FromQuery] int id, [FromQuery] int idElemento)
        {
            var listaDb = await _repository.GetByIdAsync(id, u => u.Canciones!);
            if (listaDb == null)
                return NotFound();
            var cancion = (from li in listaDb.Canciones where li.Id == idElemento select li).FirstOrDefault();
            if (cancion != null)
                return NotFound("Elemento ya insertado");
            await _repository.AddElementToColeccionAsync<Cancion>(id, idElemento, o => o.Canciones!, e => e.Listas!);
            return NoContent();
        }
    }
}
