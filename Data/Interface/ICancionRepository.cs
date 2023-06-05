using RhythmBack.Model.Models;

namespace RhythmBack.Data.Interface
{
    public interface ICancionRepository
    {
        Task<IEnumerable<Cancion>> GetByTituloAsync(string termino);
        Task<string> GetPortadaEstrenoAsync(int id);
        Task<string> GetArtistasConcatAsync(int id);
        Task<IEnumerable<Cancion>> GetExitosAsync();
        Task<DateTime> GetEstrenoAsync(int id);
        Task<IEnumerable<Cancion>> GetByTituloAsyncForLista(string termino, int idLista);
    }
}
