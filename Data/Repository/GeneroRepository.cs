using Microsoft.EntityFrameworkCore;
using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;

namespace RhythmBack.Data.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly RhythmDBContext _context;

        public GeneroRepository(RhythmDBContext context)
        {
            _context = context;
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
