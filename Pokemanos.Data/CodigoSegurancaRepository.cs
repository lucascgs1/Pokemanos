using Microsoft.EntityFrameworkCore;
using Pokemanos.Data.Interfaces;
using Pokemanos.Model;
using System.Linq;

namespace Pokemanos.Data
{
    public class CodigoSegurancaRepository : Repository<CodigoSeguranca>, ICodigoSegurancaRepository
    {
        private readonly ApplicationDbContext _context;

        public CodigoSegurancaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public CodigoSeguranca GetByUserId(int userId)
        {
            return DbSet.FirstOrDefault(x => x.UsuarioId == userId);
        }

        public void DeleteByUserId(int userId)
        {
            var codigoSeguranca = DbSet.AsNoTracking().Where(e => e.UsuarioId == userId);

            DbSet.RemoveRange(codigoSeguranca);
            SaveChanges();
        }
    }
}