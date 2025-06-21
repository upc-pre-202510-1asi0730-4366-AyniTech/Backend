using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Interfaces.REST.Resources;

namespace Lot.Reports.Interfaces.REST.Transform;

public static class ReportResourceFromEntityAssembler
{
    public static ReportResource ToResource(Report report)
    {
        return new ReportResource(
            report.Id,
            report.FieldName,
            report.FilterStartDate,
            report.FilterEndDate
        );
    }
}