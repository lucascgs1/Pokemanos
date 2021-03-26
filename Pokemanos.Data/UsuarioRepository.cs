using Pokemanos.Data.Interfaces;
using Pokemanos.Model;

namespace Pokemanos.Data
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}