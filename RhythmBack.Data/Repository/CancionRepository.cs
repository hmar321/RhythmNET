using Microsoft.EntityFrameworkCore;
using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;
using RhythmBack.Model.Util;

namespace RhythmBack.Data.Repository
{
    public class CancionRepository : ICancionRepository
    {
        private readonly RhythmDBContext _context;
        public CancionRepository(RhythmDBContext context)
        {
            _context = context;
        }

        public async Task<string> GetArtistasConcatAsync(int id)
        {

            var titulos = await (from ca in _context.Canciones
                                 where ca.Id == id
                                 from ar in ca.Artistas!
                                 select ar.Titulo).Distinct().ToListAsync();
            var resultado = String.Join(", ", titulos);
            return resultado;

        }

        public async Task<IEnumerable<Cancion>> GetByTituloAsync(string termino)
        {
            var todos = await _context.Canciones!.Include(ca => ca.Artistas).Include(ca => ca.Albums).ToListAsync();
            var canciones = todos
                .Select(ca => new { Cancion = ca, Distance = Format.FormatearTexto(ca.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) })
                .Where(x =>
                    Format.FormatearTexto(x.Cancion.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Format.FormatearTexto(x.Cancion.Artistas!.FirstOrDefault()!.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Format.FormatearTexto(x.Cancion.Albums!.FirstOrDefault()!.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderByDescending(x => x.Distance)
                .Take(10)
                .Select(x => x.Cancion).Distinct().ToList();
            return canciones;
        }

        public async Task<string> GetPortadaEstrenoAsync(int id)
        {
            var portada = await (from ca in _context.Canciones
                                 from al in ca.Albums!
                                 where ca.Id == id
                                 orderby al.Estreno
                                 select al.Portada).FirstOrDefaultAsync();
            return portada!;
        }

        public async Task<IEnumerable<Cancion>> GetExitosAsync()
        {
            var exitos = (from ca in _context.Canciones
                          orderby ca.Visitas descending
                          select ca).Take(10);
            return await exitos.ToListAsync();
        }

        public async Task<DateTime> GetEstrenoAsync(int id)
        {
            var estreno = await (from ca in _context.Canciones
                                 where ca.Id == id
                                 from al in ca.Albums!
                                 select al.Estreno).FirstOrDefaultAsync();
            return estreno ?? DateTime.MinValue;
        }
    }
}
