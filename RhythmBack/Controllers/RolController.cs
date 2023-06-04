using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RhythmBack.Data.Interface;
using RhythmBack.Data.Repository;
using RhythmBack.Model.Context;
using RhythmBack.Model.DTO;
using RhythmBack.Model.Models;
using System.Data;

namespace RhythmBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RhythmDBContext _dbContext;
        private readonly Repository<Rol> _repository;
        private readonly RolRepository _rolRepository;
        public RolController(RhythmDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _repository = new Repository<Rol>(_dbContext);
            _rolRepository = new RolRepository(_dbContext);
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol[]>>> GetAll()
        {
            var roles = await _repository.GetAllAsync();
            var rolesDto = _mapper.Map<IEnumerable<RolDTO>>(roles);
            if (rolesDto == null)
                return NotFound();
            return Ok(rolesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rol>> GetById(int id)
        {
            var rol = await _repository.GetByIdAsync(id);
            var rolDto = _mapper.Map<RolDTO>(rol);
            if (rolDto == null)
            {
                return NotFound();
            }
            return Ok(rolDto);
        }
        [HttpPost]
        public async Task<ActionResult<Rol>> Create(Rol rol)
        {
            await _repository.AddAsync(rol);
            return CreatedAtAction(nameof(GetById), new { id = rol.Id }, rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(rol);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _repository.GetByIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(rol);
            return NoContent();
        }

    }
}
