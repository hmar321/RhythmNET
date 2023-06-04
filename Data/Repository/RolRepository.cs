using RhythmBack.Data.Interface;
using RhythmBack.Model.Context;

namespace RhythmBack.Data.Repository
{
    public class RolRepository : IRolRepository
    {
        private readonly RhythmDBContext _context;
        public RolRepository(RhythmDBContext context)
        {
            _context = context;
        }
    }
}
