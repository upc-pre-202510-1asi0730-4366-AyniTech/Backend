namespace Lot.Inventaries.Interfaces.REST.Resources;

/// <summary>
/// Recurso de inventario (lote) para la API REST
/// </summary>
/// <param name="Id">
/// Identificador único del inventario
/// </param>
/// <param name="Category">
/// Categoría del producto
/// </param>
/// <param name="Product">
/// Nombre del producto
/// </param>
/// <param name="EntryDate">
/// Fecha de ingreso al inventario
/// </param>
/// <param name="Quantity">
/// Cantidad del producto
/// </param>
/// <param name="UnitPrice">
/// Precio por unidad
/// </param>
/// <param name="MinStock">
/// Stock mínimo permitido
/// </param>
/// <param name="Unit">
/// Unidad de medida
/// </param>
/// <param name="Supplier">
/// Proveedor del producto
/// </param>
public record InventarieResource(
    int Id,
    string Category,
    string Product,
    DateTime EntryDate,
    int Quantity,
    decimal UnitPrice,
    int MinStock,
    string Unit,
    string Supplier
);