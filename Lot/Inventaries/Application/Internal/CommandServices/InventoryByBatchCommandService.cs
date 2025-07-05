//L
using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.Inventaries.Domain.Model.Commands;
using Lot.Inventaries.Domain.Repositories;
using Lot.Inventaries.Domain.Services;
using Lot.Shared.Domain.Repositories;

namespace Lot.Inventaries.Application.Internal.CommandServices;

public class InventoryByBatchCommandService(
    IInventoryByBatchRepository repository,
    IUnitOfWork unitOfWork) : IInventoryByBatchCommandService
{
    public async Task<InventoryByBatch?> Handle(CreateInventoryByBatchCommand command)
    {
        var inventory = new InventoryByBatch(
            command.Proveedor,
            command.Producto,
            command.FechaEntrada,
            command.Cantidad,
            command.Precio,
            command.Unidad
        );

        try
        {
            await repository.AddAsync(inventory);
            await unitOfWork.CompleteAsync();
            return inventory;
        }
        catch (Exception)
        {
            return null;
        }
    }
}