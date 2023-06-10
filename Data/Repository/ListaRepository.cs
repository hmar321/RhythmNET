using Microsoft.EntityFrameworkCore;
using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;
using RhythmBack.Model.Util;

namespace RhythmBack.Data.Repository
{
    public class ListaRepository : IListaRepository
    {
        private readonly RhythmDBContext _context;
        public ListaRepository(RhythmDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Lista>> GetByTituloAsync(string termino, int id)
        {
            var todos = await _context.Listas!.Include(li => li.Canciones).Include(li=>li.Creador).ToListAsync();
            var listas = todos
                .Where(li => li.Creador!.Id != id && li.Titulo!= "Favoritos")
                .Select(li => new { Lista = li, Distance = Format.FormatearTexto(li.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) })
                .Where(x => 
                    Format.FormatearTexto(x.Lista.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Format.FormatearTexto(x.Lista.Creador!.Nick!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderByDescending(x => x.Distance)
                .Take(10)
                .Select(x => x.Lista).Distinct().ToList();
            return listas;
        }

        public async Task<IEnumerable<Lista>> GetExitosAsync(int id)
        {
            var exitos = (from li in _context.Listas!.Include(li => li.Canciones).Include(li => li.Creador)
                          where li.Titulo!= "Favoritos"
                          where li.CreadorId != id
                          orderby li.Visitas descending
                          select li).Take(10);

            return await exitos.ToListAsync();
        }
        public async Task<string> GetArtistasConcatByCancionIdAsync(int id)
        {
            var titulos = await (from ca in _context.Canciones
                                 where ca.Id == id
                                 from al in ca.Albums!
                                 from ar in ca.Artistas!
                                 select ar.Titulo).Distinct().ToListAsync();
            var resultado = String.Join(", ", titulos);
            int index = resultado.IndexOf(",");
            if (index >= 0)
            {
                resultado = resultado.Remove(index, 1);
                resultado = resultado.Insert(index, @" ft.");
            }
            return resultado;
        }
    }
}
