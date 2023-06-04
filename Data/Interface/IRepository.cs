using RhythmBack.Model.Models;
using System.Linq.Expressions;

namespace RhythmBack.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task RemoveElementFromColeccionAsync<TElemento>(int idEntidad, int idElemento, Expression<Func<T, ICollection<TElemento>>> coleccionActualizarEnt, Expression<Func<TElemento, ICollection<T>>> coleccionActualizarEle) where TElemento : class, IEntidad;
        Task AddElementToColeccionAsync<TElemento>(int idEntidad, int idElemento, Expression<Func<T, ICollection<TElemento>>> coleccionActualizarEnt, Expression<Func<TElemento, ICollection<T>>> coleccionActualizarEle) where TElemento : class, IEntidad;
    }
}
