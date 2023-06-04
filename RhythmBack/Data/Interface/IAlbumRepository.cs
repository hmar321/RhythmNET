using RhythmBack.Model.Models;

namespace RhythmBack.Data.Interface
{
    public interface IAlbumRepository
    {
        Task<IEnumerable<Album>> GetByTituloAsync(string termino);
        Task<string> GetArtistasConcatByAlbumIdAsync(int id);
        Task<IEnumerable<Album>> GetExitosAsync();
        Task<string> GetArtistasConcatByCancionIdAsync(int id);
    }
}
