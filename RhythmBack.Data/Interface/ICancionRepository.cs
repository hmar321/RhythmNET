using RhythmBack.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBack.Data.Interface
{
    public interface ICancionRepository
    {
        Task<IEnumerable<Cancion>> GetByTituloAsync(string termino);
        Task<string> GetPortadaEstrenoAsync(int id);
        Task<string> GetArtistasConcatAsync(int id);
        Task<IEnumerable<Cancion>> GetExitosAsync();
        Task<DateTime> GetEstrenoAsync(int id);
        
    }
}
