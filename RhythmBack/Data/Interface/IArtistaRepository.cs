using RhythmBack.Model.Models;

namespace RhythmBack.Data.Interface
{
    public interface IArtistaRepository
    {
        Task<IEnumerable<Artista>> GetByTituloAsync(string termino);
        Task<IEnumerable<Album>> GetAlbumsByArtistaId(int id);
        Task<IEnumerable<Artista>> GetExitosAsync();
        Task<string> GetArtistasTituloConcatByAlbumIdAsync(int id);
        Task<IEnumerable<Cancion>> GetCancionesByAlbumIdAsync(int id);
        Task<string> GetArtistasTituloConcatByCancionIdAsync(int id);
    }
}
