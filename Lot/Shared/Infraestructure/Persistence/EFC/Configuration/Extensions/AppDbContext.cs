using Lot.Inventaries.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     Contexto de base de datos para la aplicación Lot
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configuración de Inventary
        builder.Entity<Inventary>().HasKey(i => i.Id);
        builder.Entity<Inventary>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Inventary>().OwnsOne(i => i.Category, c =>
        {
            c.Property(p => p.Name).HasColumnName("CategoryName");
        });
        builder.Entity<Inventary>().OwnsOne(i => i.Product, p =>
        {
            p.Property(pp => pp.Name).HasColumnName("ProductName");
        });
        builder.Entity<Inventary>().OwnsOne(i => i.Unit, u =>
        {
            u.Property(pu => pu.Name).HasColumnName("UnitName");
        });
        builder.Entity<Inventary>().OwnsOne(i => i.Supplier, s =>
        {
            s.Property(ps => ps.Name).HasColumnName("SupplierName");
        });
    }
}