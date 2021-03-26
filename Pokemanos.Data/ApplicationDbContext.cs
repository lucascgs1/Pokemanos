using Microsoft.EntityFrameworkCore;
using Pokemanos.Model;

namespace Pokemanos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
