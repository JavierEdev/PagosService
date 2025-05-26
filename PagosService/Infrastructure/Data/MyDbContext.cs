using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PagosService.Domain.Entities;

namespace PagosService.Infrastructure.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public DbSet<PagoRecord> Pagos { get; set; } = null!;
    public DbSet<ReservaRecord> Reservas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PagoRecord>().ToTable("pago");
        modelBuilder.Entity<PagoRecord>().HasKey(p => p.IdPago);
    }
}
