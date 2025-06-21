
using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Domain.Repositories;
using Lot.Reports.Domain.Services;

namespace Lot.Reports.Application.Internal.QueryServices;

public class CategoryReportQueryService : ICategoryReportQueryService
{
    private readonly ICategoryReportRepository _repository;

    public CategoryReportQueryService(ICategoryReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryReport>> ListAsync()
    {
        return await _repository.ListAsync();
    }
}