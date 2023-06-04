using RhythmBack.Model.Models;

namespace RhythmBack.Data.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByEmailAndPassord(string email, string password);

    }
}
