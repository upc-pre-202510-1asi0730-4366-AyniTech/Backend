using Lot.IAM.Domain.Model.Aggregates;
using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.ProductManagement.Domain.Model.Aggregates;
using Lot.Reports.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;


namespace Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;


/// <summary>
///     Contexto de base de datos para la aplicación Lot
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // En el método OnModelCreating
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
        builder.Entity<User>().Property(u => u.LastName).IsRequired();
        builder.Entity<User>().Property(u => u.Password).IsRequired();
        builder.Entity<User>().Property(u => u.Email).IsRequired();

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
        builder.Entity<Inventary>().HasKey(i => i.Id);

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
        
        // Configuración de Report  
        builder.Entity<Report>().HasKey(r => r.Id);
        builder.Entity<Report>().Property(r => r.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

// Configurar objeto de valor ReportField
        builder.Entity<Report>().OwnsOne(r => r.Field, f =>
        {
            f.Property(p => p.Name)
                .HasColumnName("FieldName")
                .IsRequired()
                .HasMaxLength(100);
        });

// Configurar objeto de valor ReportFilter
        builder.Entity<Report>().OwnsOne(r => r.Filter, f =>
        {
            f.Property(p => p.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();

            f.Property(p => p.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();
        });

        builder.Entity<Report>().ToTable("reports");
        
        // StockAverageReport
        builder.Entity<StockAverageReport>().HasKey(s => s.Id);

        builder.Entity<StockAverageReport>()
            .Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedNever(); // Porque tú defines el ID en el constructor

        builder.Entity<StockAverageReport>()
            .Property(s => s.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<StockAverageReport>()
            .Property(s => s.Product)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<StockAverageReport>()
            .Property(s => s.AverageQuantity)
            .IsRequired();

        builder.Entity<StockAverageReport>()
            .Property(s => s.IdealStock)
            .IsRequired();

        builder.Entity<StockAverageReport>()
            .Property(s => s.QueryDate)
            .IsRequired();

        builder.Entity<StockAverageReport>()
            .Property(s => s.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<StockAverageReport>()
            .ToTable("stock_average_reports");


// CategoryReport
        builder.Entity<CategoryReport>().HasKey(c => c.Id);
        builder.Entity<CategoryReport>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<CategoryReport>().Property(c => c.Category).IsRequired().HasMaxLength(100);
        builder.Entity<CategoryReport>().Property(c => c.Product).IsRequired().HasMaxLength(100);
        builder.Entity<CategoryReport>().Property(c => c.Quantity).IsRequired();
        builder.Entity<CategoryReport>().Property(c => c.UnitPrice).HasColumnType("decimal(10,2)");
        builder.Entity<CategoryReport>().Property(c => c.Total).HasColumnType("decimal(10,2)");
        builder.Entity<CategoryReport>().Property(c => c.QueryDate).HasColumnType("date");
        builder.Entity<CategoryReport>().Property(c => c.ReportId).IsRequired();
        builder.Entity<CategoryReport>()
            .HasOne(c => c.Report)
            .WithMany()
            .HasForeignKey(c => c.ReportId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Entity<CategoryReport>().ToTable("category_reports");


        

        // Configuración de Combos
        builder.Entity<Combo>().HasKey(c => c.Id);
        builder.Entity<Combo>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Combo>().Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Combo>().ToTable("combos");

        // ComboItems
        builder.Entity<ComboItem>().HasKey(ci => ci.Id);
        builder.Entity<ComboItem>().Property(ci => ci.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ComboItem>().Property(ci => ci.ComboId).HasColumnName("ComboId");
        builder.Entity<ComboItem>().Property(ci => ci.ProductId).HasColumnName("ProductId");
        builder.Entity<ComboItem>().Property(ci => ci.Quantity).IsRequired();

        // Relaciones de ComboItem
        builder.Entity<ComboItem>()
            .HasOne(ci => ci.Combo)
            .WithMany(c => c.ComboItems)
            .HasForeignKey(ci => ci.ComboId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ComboItem>()
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ComboItem>().ToTable("combo_items");
    }
}