using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.Inventaries.Domain.Model.ValueOjbects;
using Lot.Inventaries.Domain.Repositories;
using Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;
using Lot.Shared.Infraestructure.Persistence.EFC.Repositories;

namespace Lot.Inventaries.Infraestructure.Persistence.EFC.Repositories;

/// <summary>
/// Implementación del repositorio de inventario (lote)
/// </summary>
/// <param name="context">El contexto de base de datos</param>
public class LotRepository(AppDbContext context)
    : BaseRepository<Inventary>(context), IInventaryRepository
{
    public async Task<Inventary?> FindInventaryByProductNameAsync(ProductName productName)
    {
        return Context.Set<Inventary>().FirstOrDefault(i => i.Product == productName);
    }

    public async Task<IEnumerable<Inventary>> FindAllInventariesAsync()
    {
        return Context.Set<Inventary>().ToList();
    }

    public async Task<IEnumerable<Inventary>> GetAllLotByEntryDate(DateTime startDate)
    {
        return Context.Set<Inventary>().Where(i => i.EntryDate == startDate).ToList();
    }

    public async Task<IEnumerable<Inventary>> GetAllLotByPrice(decimal price)
    {
        return Context.Set<Inventary>().Where(i => i.UnitPrice == price).ToList();
    }

    public async Task<IEnumerable<Inventary>> GetAllLotByProduct(ProductName product)
    {
        return Context.Set<Inventary>().Where(i => i.Product == product).ToList();
    }

    public async Task<IEnumerable<Inventary>> GetAllLotBySupplier(Supplier supplier)
    {
        return Context.Set<Inventary>().Where(i => i.Supplier == supplier).ToList();
    }

    public async Task<IEnumerable<Inventary>> GetAlLotByQuantity(int quantity)
    {
        return Context.Set<Inventary>().Where(i => i.Quantity == quantity).ToList();
    }
}