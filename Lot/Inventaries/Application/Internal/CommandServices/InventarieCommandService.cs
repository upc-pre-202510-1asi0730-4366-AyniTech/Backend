using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.Inventaries.Domain.Model.Commands;
using Lot.Inventaries.Domain.Repositories;
using Lot.Inventaries.Domain.Services;
using Lot.Shared.Domain.Repositories;

namespace Lot.Inventaries.Application.Internal.CommandServices;

/// <summary>
/// Servicio de comandos para Inventario
/// </summary>
/// <param name="inventaryRepository">
/// Repositorio de inventario
/// </param>
/// <param name="unitOfWork">
/// Unidad de trabajo
/// </param>
public class InventarieCommandService(
    IInventaryRepository inventaryRepository,
    IUnitOfWork unitOfWork)
    : IInventaryCommandService
{
    /// <inheritdoc />
    public async Task<Inventary?> Handle(CreateInventaryCommand command)
    {
        var inventary = new Inventary(command);
        try
        {
            await inventaryRepository.AddAsync(inventary);
            await unitOfWork.CompleteAsync();
            return inventary;
        }
        catch (Exception)
        {
           
            return null;
        }
    }
}