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

    }
}
