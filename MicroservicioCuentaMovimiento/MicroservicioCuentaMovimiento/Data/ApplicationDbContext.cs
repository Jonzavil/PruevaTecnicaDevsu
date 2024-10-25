using Microsoft.EntityFrameworkCore;
using MicroservicioCuentaMovimiento.Models;

namespace MicroservicioCuentaMovimiento.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .HasKey(c => c.NumeroCuenta);

            modelBuilder.Entity<Movimiento>()
                .HasOne(c => c.Cuenta)
                .WithMany(c => c.Movimientos)
                .HasForeignKey(m => m.NumeroCuenta);

            base.OnModelCreating(modelBuilder);
        }
    }
}
