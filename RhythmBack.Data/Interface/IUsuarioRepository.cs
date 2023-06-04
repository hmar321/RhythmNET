using RhythmBack.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmBack.Data.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByEmailAndPassord(string email, string password);

    }
}
