using Lot.Reports.Domain.Model.Aggregates;

namespace Lot.Reports.Domain.Repositories;

public interface ICategoryReportRepository
{
    Task<IEnumerable<CategoryReport>> ListAsync();
}