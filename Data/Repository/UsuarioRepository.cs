using Microsoft.EntityFrameworkCore;
using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;
using RhythmBack.Model.Models;

namespace RhythmBack.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RhythmDBContext _context;

        public UsuarioRepository(RhythmDBContext context)
        {
            _context = context;
        }
        public async Task<Usuario> GetByEmailAndPassord(string email, string password)
        {
            var usuario = await _context.Usuarios!
                    .Include(u => u.Albums)
                    .Include(u => u.Artistas)
                    .Include(u => u.Listas)
                    .Include(u => u.ListasCreadas)
                    .Include(u => u.Rol)
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return usuario!;
        }

    }
}
