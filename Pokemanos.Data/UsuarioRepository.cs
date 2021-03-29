using Microsoft.EntityFrameworkCore;
using Pokemanos.Data.Interfaces;
using Pokemanos.Model;
using System.Threading.Tasks;

namespace Pokemanos.Data
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Usuario> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}