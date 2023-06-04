using Microsoft.EntityFrameworkCore;
using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;
using RhythmBack.Model.Util;

namespace RhythmBack.Data.Repository
{
    public class ArtistaRepository : IArtistaRepository
    {
        private readonly RhythmDBContext _context;

        public ArtistaRepository(RhythmDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Artista>> GetByTituloAsync(string termino)
        {
            var todos = await _context.Artistas!.Include(ar=>ar.Canciones)!.ThenInclude(ca=>ca.Albums).ToListAsync();
            var artistas = todos
                .Select(ar => new { Artista = ar, Distance = Format.FormatearTexto(ar.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) })
                .Where(x => 
                Format.FormatearTexto(x.Artista.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0 ||
                Format.FormatearTexto(x.Artista.Canciones!.FirstOrDefault()!.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0 ||
                Format.FormatearTexto(x.Artista.Canciones!.FirstOrDefault()!.Albums!.FirstOrDefault()?.Titulo!).IndexOf(Format.FormatearTexto(termino), StringComparison.OrdinalIgnoreCase) >= 0
                )
                .OrderByDescending(x => x.Distance)
                .Take(10)
                .Select(x => x.Artista).Distinct().ToList();
            artistas.ForEach(ar => ar.Canciones = null);
            return artistas;
        }

        public async Task<IEnumerable<Album>> GetAlbumsByArtistaId(int id)
        {
            var albums =await (from ar in _context.Artistas
                          where ar.Id == id
                          from ca in ar.Canciones!
                          from al in ca.Albums!
                          orderby al.Estreno
                          select al).Distinct().ToListAsync();
            albums.ForEach(al => al.Canciones = null);
            return albums;
        }

        public async Task<IEnumerable<Artista>> GetExitosAsync()
        {
            var exitos = (from ar in _context.Artistas
                          orderby ar.Visitas descending
                          select ar).Take(10);
            return await exitos.ToListAsync();
        }

        public async Task<string> GetArtistasTituloConcatByAlbumIdAsync(int id)
        {
            var titulos = await (from al in _context.Albums
                                 from ca in al.Canciones!
                                 from ar in ca.Artistas!
                                 where al.Id == id
                                 select ar.Titulo).Distinct().ToListAsync();
            var resultado = String.Join(", ", titulos);
            return resultado;
        }

        public async Task<IEnumerable<Cancion>> GetCancionesByAlbumIdAsync(int id)
        {
            var cancionesAlbum = (from al in _context.Albums
                                  from ca in al.Canciones!
                                  where al.Id == id
                                  select ca).Distinct();
            return await cancionesAlbum.ToListAsync();
        }

        public async Task<string> GetArtistasTituloConcatByCancionIdAsync(int id)
        {
            var titulos = await (from ca in _context.Canciones
                                 from al in ca.Albums!
                                 from ar in ca.Artistas!
                                 where ca.Id == id
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
