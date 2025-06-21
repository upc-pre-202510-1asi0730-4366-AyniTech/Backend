using Lot.IAM.Domain.Model.Aggregates;
using Lot.IAM.Domain.Model.Entities;
using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.ProductManagement.Domain.Model.Aggregates;
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

        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Name).IsRequired();
        builder.Entity<User>().Property(u => u.Password).IsRequired();
        builder.Entity<User>().Property(u => u.Email).IsRequired();
        
        builder.Entity<PaymentCard>().HasKey(p => p.Id);
        builder.Entity<PaymentCard>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PaymentCard>().Property(p => p.CardNumber).IsRequired();
        builder.Entity<PaymentCard>().Property(p => p.ExpiryDate).IsRequired();
        builder.Entity<PaymentCard>().Property(p => p.CVV).IsRequired().HasColumnName("cvv");
        builder.Entity<PaymentCard>()
            .HasOne(p => p.User)
            .WithOne(u => u.PaymentCard)
            .HasForeignKey<PaymentCard>(p => p.UserId);
        
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

        // Configuración de ProductManagement entities
        
        // Categories
        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Category>().ToTable("categories");

        // Units
        builder.Entity<Unit>().HasKey(u => u.Id);
        builder.Entity<Unit>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Unit>().Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Unit>().Property(u => u.Abbreviation).IsRequired().HasMaxLength(10);
        builder.Entity<Unit>().ToTable("units_of_measure");

        // Tags
        builder.Entity<Tag>().HasKey(t => t.Id);
        builder.Entity<Tag>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Tag>().Property(t => t.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Tag>().ToTable("tags");

        // Products
        builder.Entity<Product>().HasKey(p => p.Id);
        builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Product>().Property(p => p.Description).HasColumnType("text");
        builder.Entity<Product>().Property(p => p.PurchasePrice).HasColumnType("decimal(10,2)");
        builder.Entity<Product>().Property(p => p.SalePrice).HasColumnType("decimal(10,2)");
        builder.Entity<Product>().Property(p => p.InternalNotes).HasColumnType("text");
        
        // Propiedades simples para las claves foráneas
        builder.Entity<Product>().Property(p => p.CategoryId).HasColumnName("CategoryId");
        builder.Entity<Product>().Property(p => p.UnitId).HasColumnName("UnitId");

        // Relaciones de Product
        builder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Product>()
            .HasOne(p => p.Unit)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Product>().ToTable("products");

        // ProductTags (tabla intermedia)
        builder.Entity<ProductTag>().HasKey(pt => pt.Id);
        builder.Entity<ProductTag>().Property(pt => pt.Id).IsRequired().ValueGeneratedOnAdd();
        
        // Propiedades simples para las claves foráneas
        builder.Entity<ProductTag>().Property(pt => pt.ProductId).HasColumnName("ProductId");
        builder.Entity<ProductTag>().Property(pt => pt.TagId).HasColumnName("TagId");

        // Relaciones de ProductTag
        builder.Entity<ProductTag>()
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(pt => pt.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProductTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.ProductTags)
            .HasForeignKey(pt => pt.TagId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ProductTag>().ToTable("product_tags");

        // Índice compuesto para evitar duplicados en ProductTag
        builder.Entity<ProductTag>()
            .HasIndex(pt => new { pt.ProductId, pt.TagId })
            .IsUnique();
    }
}