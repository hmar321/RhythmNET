using Microsoft.EntityFrameworkCore;
using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;
using System.Linq.Expressions;

namespace RhythmBack.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntidad
    {
        private readonly RhythmDBContext _context;

        public Repository(RhythmDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entidad = await query.FirstOrDefaultAsync(o => o.Id == id);
            return entidad!;
        }

        public async Task RemoveElementFromColeccionAsync<TElemento>(int idEntidad, int idElemento, Expression<Func<T, ICollection<TElemento>>> coleccionActualizarEnt, Expression<Func<TElemento, ICollection<T>>> coleccionActualizarEle) where TElemento : class, IEntidad
        {
            var entidad = await _context.Set<T>().FirstOrDefaultAsync(o => o.Id == idEntidad);
            var elemento = await _context.Set<TElemento>().FirstOrDefaultAsync(o => o.Id == idElemento);
            var coleccionEntidad = coleccionActualizarEnt.Compile()(entidad!);
            var coleccionElemento = coleccionActualizarEle.Compile()(elemento!);
            coleccionEntidad.Remove(elemento!);
            coleccionElemento.Remove(entidad!);
            await _context.SaveChangesAsync();
        }

        public async Task AddElementToColeccionAsync<TElemento>(int idEntidad, int idElemento, Expression<Func<T, ICollection<TElemento>>> coleccionActualizarEnt, Expression<Func<TElemento, ICollection<T>>> coleccionActualizarEle) where TElemento : class, IEntidad
        {
            var entidad = await _context.Set<T>().FirstOrDefaultAsync(o => o.Id == idEntidad);
            var elemento = await _context.Set<TElemento>().FirstOrDefaultAsync(o => o.Id == idElemento);
            var coleccionEntidad = coleccionActualizarEnt.Compile()(entidad!);
            var coleccionElemento = coleccionActualizarEle.Compile()(elemento!);

            if (coleccionEntidad == null)
                coleccionEntidad = new List<TElemento>();

            if (coleccionElemento == null)
                coleccionElemento = new List<T>();

            coleccionEntidad.Add(elemento!);
            coleccionElemento.Add(entidad!);

            await _context.SaveChangesAsync();
        }
    }
}
