using Lot.AlertStockManagement.Domain.Model.Queries;
using Lot.AlertStockManagement.Domain.Model.Aggregates;
using Lot.AlertStockManagement.Domain.Repositories;
using Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;
using Lot.Inventaries.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Lot.AlertStockManagement.Infrastructure.Persistences.EFC.Repositories;

public class InventoryReadRepository : IInventoryReadRepository
{
    private readonly AppDbContext _context;

    public InventoryReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StockAlertItem>> GetStockAlertsAsync(StockAlertQuery query)
    {
        var alerts = await _context.Set<Inventary>()
            .Where(i => query.IncludeLowStock && i.Quantity <= i.MinStock)
            .Select(i => new StockAlertItem
            {
                ProductName = i.Product.Name, // Usa ToString() si i.Product no tiene .Name
                Quantity = i.Quantity,
                MinStock = i.MinStock,
                EntryDate = i.EntryDate,
                IsLowStock = i.Quantity <= i.MinStock
            })
            .ToListAsync();

        return alerts;
    }
}
