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
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasOne(s => s.CodigoSeguranca);
            });
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<CodigoSeguranca> CodigoSeguranca { get; set; }
    }
}
