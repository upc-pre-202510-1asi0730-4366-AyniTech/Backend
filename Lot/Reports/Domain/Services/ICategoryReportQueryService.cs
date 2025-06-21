// ICategoryReportQueryService.cs
using Lot.Reports.Domain.Model.Aggregates;

namespace Lot.Reports.Domain.Services;

public interface ICategoryReportQueryService
{
    Task<IEnumerable<CategoryReport>> ListAsync();
}