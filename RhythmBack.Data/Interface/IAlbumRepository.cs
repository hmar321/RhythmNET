using RhythmBack.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
