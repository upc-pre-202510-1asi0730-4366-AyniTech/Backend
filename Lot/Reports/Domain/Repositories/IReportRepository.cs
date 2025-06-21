using Lot.Reports.Domain.Model.Aggregates;

namespace Lot.Reports.Domain.Repositories;

public interface IReportRepository
{
    Task AddAsync(Report report);

    Task<IEnumerable<Report>> ListAsync();
    
    Task<IEnumerable<Report>> ListByCategoryAsync();

    Task<IEnumerable<Report>> ListByProductAsync();
    
    Task<IEnumerable<StockAverageReport>> ListStockAverageReportsAsync();
}