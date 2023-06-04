using Microsoft.AspNetCore.Mvc;
using RhythmBack.Data.Repository;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;
using RhythmBack.Model.DTO;
using AutoMapper;

namespace RhythmBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RhythmDBContext _dbContext;
        private readonly Repository<Genero> _repository;
        private readonly GeneroRepository _repositoryRepository;

        public GeneroController(RhythmDBContext context,IMapper mapper)
        {

            _dbContext = context;
            _repository = new Repository<Genero>(_dbContext);
            _repositoryRepository=new GeneroRepository(_dbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetAll()
        {
            var generos = await _repository.GetAllAsync(g => g.Canciones!);
            if (generos == null)
                return NotFound();
            var generosDto = _mapper.Map<IEnumerable<GeneroDTO>>(generos);
            return Ok(generosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> GetById(int id)
        {
            var genero = await _repository.GetByIdAsync(id, g => g.Canciones!);
            if (genero == null)
                return NotFound();
            var generoDto = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDto);
        }
        [HttpPost]
        public async Task<ActionResult<Genero>> Create(Genero genero)
        {
            await _repository.AddAsync(genero);
            return CreatedAtAction(nameof(GetById), new { id = genero.Id }, genero);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Genero genero)
        {
            if (id != genero.Id)
                return BadRequest();
            await _repository.UpdateAsync(genero);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genero = await _repository.GetByIdAsync(id);
            if (genero == null)
                return NotFound();
            await _repository.DeleteAsync(genero);
            return NoContent();
        }
    }
}
