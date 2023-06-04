using RhythmBack.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBack.Data.Interface
{
    public interface IListaRepository
    {
        Task<IEnumerable<Lista>> GetByTituloAsync(string termino, int id);
        Task<IEnumerable<Lista>> GetExitosAsync(int id);
    }
}
