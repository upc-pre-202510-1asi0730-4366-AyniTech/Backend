using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Domain.Repositories;
using Lot.Reports.Domain.Services;

namespace Lot.Reports.Application.Internal.QueryServices;

public class StockAverageQueryService : IStockAverageQueryService
{
    private readonly IReportRepository _reportRepository;

    public StockAverageQueryService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<IEnumerable<StockAverageReport>> ListStockAverageReportsAsync()
    {
        return await _reportRepository.ListStockAverageReportsAsync();
    }
}