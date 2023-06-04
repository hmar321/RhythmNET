using Microsoft.EntityFrameworkCore;
using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;
using RhythmBack.Model.Util;

namespace RhythmBack.Data.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly RhythmDBContext _context;

        public AlbumRepository(RhythmDBContext context)
        {
            _context = context;
        }

        public async Task<string> GetArtistasConcatByAlbumIdAsync(int id)
        {
            var titulos = await (from al in _context.Albums
                                 where al.Id == id
                                 from ca in al.Canciones!
                                 from ar in ca.Artistas!
                                 select ar.Titulo).Distinct().ToListAsync();
            var resultado = String.Join(", ", titulos);
            return resultado;
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

        public async Task<IEnumerable<Album>> GetByTituloAsync(string termino)
        {
            var todos = await _context.Albums!.Include(al=>al.Canciones!).ThenInclude(ca=>ca.Artistas).ToListAsync();
            var albums = todos
                .Select(al => new { Album = al, Distance = Format.FormatearTexto(al.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) })
                .Where(x => 
                Format.FormatearTexto(x.Album.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0 ||
                Format.FormatearTexto(x.Album.Canciones!.FirstOrDefault()?.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0 ||
                Format.FormatearTexto(x.Album.Canciones!.FirstOrDefault()?.Artistas!.FirstOrDefault()?.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0
                )
                .OrderByDescending(x => x.Distance)
                .Take(10)
                .Select(x => x.Album).Distinct().ToList();
            albums.ForEach(al => al.Canciones =null);
            return albums;
        }

        public async Task<IEnumerable<Album>> GetExitosAsync()
        {
            var exitos = (from al in _context.Albums!
                          orderby al.Visitas descending
                          select al).Take(10);
            return await exitos.ToListAsync();
        }
    }
}
