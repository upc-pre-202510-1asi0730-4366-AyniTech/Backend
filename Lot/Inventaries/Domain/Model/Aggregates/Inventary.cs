using Lot.Inventaries.Domain.Model.Commands;
using Lot.Inventaries.Domain.Model.ValueOjbects;

namespace Lot.Inventaries.Domain.Model.Aggregates;

/// <summary>
/// Inventory Aggregate Root 
/// </summary>
/// <remarks>
/// This class represents the Inventory aggregate root.
/// It contains the properties and methods to manage the inventory information.
/// </remarks>
public partial class Inventary
{
    public int Id { get; }
    public Category Category { get; private set; }
    public ProductName Product { get; private set; }
    public DateTime EntryDate { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int MinStock { get; private set; }
    public Unit Unit { get; private set; }
    public Supplier Supplier { get; private set; }
    
    public string FullProductName => Product.Name;
    public string CategoryName => Category.Name;
    public string SupplierName => Supplier.Name;
    public string UnitName => Unit.Name;

    public Inventary()
    {
        Category = new Category();
        Product = new ProductName();
        Unit = new Unit();
        Supplier = new Supplier();
        EntryDate = DateTime.UtcNow;
    }
    
    public Inventary(string category, string product, DateTime entryDate, int quantity, 
        decimal unitPrice, int minStock, string unit, string supplier)
    {
        Category = new Category(category);
        Product = new ProductName(product);
        EntryDate = entryDate;
        Quantity = quantity;
        UnitPrice = unitPrice;
        MinStock = minStock;
        Unit = new Unit(unit);
        Supplier = new Supplier(supplier);
    }

    public Inventary(CreateInventaryCommand command)
    {
        Category = new Category(command.Category);
        Product = new ProductName(command.Product);
        EntryDate = command.EntryDate;
        Quantity = command.Quantity;
        UnitPrice = command.UnitPrice;
        MinStock = command.MinStock;
        Unit = new Unit(command.Unit);
        Supplier = new Supplier(command.Supplier);
    }
}