namespace Lot.Inventaries.Domain.Model.Commands;

/// <summary>
/// Create Inventory Command 
/// </summary>
/// <param name="Category">
/// The category of the product in inventory.
/// </param>
/// <param name="Product">
/// The name of the product in inventory.
/// </param>
/// <param name="EntryDate">
/// The date when the product was added to inventory.
/// </param>
/// <param name="Quantity">
/// The quantity of the product in inventory.
/// </param>
/// <param name="UnitPrice">
/// The price per unit of the product.
/// </param>
/// <param name="MinStock">
/// The minimum stock level for the product.
/// </param>
/// <param name="Unit">
/// The unit of measurement for the product.
/// </param>
/// <param name="Supplier">
/// The supplier of the product.
/// </param>
public record CreateInventaryCommand(
    string Category,
    string Product,
    DateTime EntryDate,
    int Quantity,
    decimal UnitPrice,
    int MinStock,
    string Unit,
    string Supplier
);