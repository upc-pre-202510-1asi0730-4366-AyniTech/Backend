using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Interfaces.REST.Resources;

namespace Lot.Reports.Interfaces.REST.Transform;

public static class CategoryReportResourceFromEntityAssembler
{
    public static CategoryReportResource ToResource(CategoryReport report) =>
        new CategoryReportResource(
            report.Id,
            report.ReportId,
            report.Category,
            report.Product,
            report.Quantity,
            report.UnitPrice,
            report.Total,
            report.QueryDate.ToDateTime(TimeOnly.MinValue) 
        );

}