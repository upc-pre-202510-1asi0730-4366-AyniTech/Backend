using Lot.Reports.Domain.Model.Aggregates;

namespace Lot.Reports.Domain.Services;

public interface IStockAverageQueryService
{
    Task<IEnumerable<StockAverageReport>> ListStockAverageReportsAsync();
}