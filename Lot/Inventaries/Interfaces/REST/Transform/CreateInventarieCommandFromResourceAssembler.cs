using Lot.Inventaries.Domain.Model.Commands;
using Lot.Inventaries.Interfaces.REST.Resources;

namespace Lot.Inventaries.Interfaces.REST.Transform;

/// <summary>
/// Ensamblador para crear un comando CreateInventaryCommand a partir de un recurso CreateInventarieResource
/// </summary>
public static class CreateInventarieCommandFromResourceAssembler
{
    /// <summary>
    /// Crea un comando CreateInventaryCommand a partir de un recurso CreateInventarieResource
    /// </summary>
    /// <param name="resource">
    /// El recurso <see cref="CreateInventarieResource"/> del cual crear el comando
    /// </param>
    /// <returns>
    /// El comando <see cref="CreateInventaryCommand"/> creado a partir del recurso
    /// </returns>
    public static CreateInventaryCommand ToCommandFromResource(CreateInventarieResource resource)
    {
        return new CreateInventaryCommand(
            resource.Category,
            resource.Product,
            resource.EntryDate,
            resource.Quantity,
            resource.UnitPrice,
            resource.MinStock,
            resource.Unit,
            resource.Supplier
        );
    }
}