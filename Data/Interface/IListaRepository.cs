using RhythmBack.Model.Models;

namespace RhythmBack.Data.Interface
{
    public interface IListaRepository
    {
        Task<IEnumerable<Lista>> GetByTituloAsync(string termino, int id);
        Task<IEnumerable<Lista>> GetExitosAsync(int id);
    }
}
