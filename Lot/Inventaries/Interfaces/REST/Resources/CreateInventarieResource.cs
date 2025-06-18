namespace Lot.Inventaries.Interfaces.REST.Resources;

/// <summary>
/// Recurso para crear un nuevo inventario (lote)
/// </summary>
/// <param name="Category">
/// La categoría del producto en inventario
/// </param>
/// <param name="Product">
/// El nombre del producto en inventario
/// </param>
/// <param name="EntryDate">
/// La fecha de ingreso del producto al inventario
/// </param>
/// <param name="Quantity">
/// La cantidad del producto en inventario
/// </param>
/// <param name="UnitPrice">
/// El precio por unidad del producto
/// </param>
/// <param name="MinStock">
/// El stock mínimo permitido para el producto
/// </param>
/// <param name="Unit">
/// La unidad de medida del producto
/// </param>
/// <param name="Supplier">
/// El proveedor del producto
/// </param>
public record CreateInventarieResource(
    string Category,
    string Product,
    DateTime EntryDate,
    int Quantity,
    decimal UnitPrice,
    int MinStock,
    string Unit,
    string Supplier
);