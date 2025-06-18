using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.Inventaries.Interfaces.REST.Resources;

namespace Lot.Inventaries.Interfaces.REST.Transform;

/// <summary>
/// Ensamblador para convertir la entidad Inventary a InventarieResource
/// </summary>
public static class InventarieResourceFromEntityAssembler
{
    /// <summary>
    /// Convierte una entidad Inventary a un recurso InventarieResource
    /// </summary>
    /// <param name="entity">
    /// Entidad <see cref="Inventary"/> a convertir
    /// </param>
    /// <returns>
    /// <see cref="InventarieResource"/> convertido desde la entidad <see cref="Inventary"/>
    /// </returns>
    public static InventarieResource ToResourceFromEntity(Inventary entity)
    {
        return new InventarieResource(
            entity.Id,
            entity.Category.Name,
            entity.Product.Name,
            entity.EntryDate,
            entity.Quantity,
            entity.UnitPrice,
            entity.MinStock,
            entity.Unit.Name,
            entity.Supplier.Name
        );
    }
}