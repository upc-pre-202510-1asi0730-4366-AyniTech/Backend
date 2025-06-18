using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.Inventaries.Domain.Model.Queries;
using Lot.Inventaries.Domain.Model.ValueOjbects;
using Lot.Inventaries.Domain.Repositories;
using Lot.Inventaries.Domain.Services;

namespace Lot.Inventaries.Application.Internal.QuerysServices;

/// <summary>
/// Servicio de consultas para Inventario (Inventary)
/// </summary>
/// <param name="inventaryRepository">
/// Repositorio de inventario
/// </param>
public class InventarieQueryService(IInventaryRepository inventaryRepository) : IInvetaryQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Inventary>> Handle(GetAllLotByEntryDate query)
    {
        return await inventaryRepository.GetAllLotByEntryDate(query.StartDate);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Inventary>> Handle(GetAllLotByPrice query)
    {
        return await inventaryRepository.GetAllLotByPrice(query.Price);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Inventary>> Handle(GetAllLotByProduct query)
    {
        return await inventaryRepository.GetAllLotByProduct(query.Product);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Inventary>> Handle(GetAllLotBySupplier query)
    {
        return await inventaryRepository.GetAllLotBySupplier(query.Supplier);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Inventary>> Handle(GetAlLotByQuantity query)
    {
        return await inventaryRepository.GetAlLotByQuantity(query.Quantity);
    }
}