namespace Lot.Reports.Interfaces.REST.Resources;

public record CategoryReportResource(
    int Id,
    int ReportId,
    string Category,
    string Product,
    int Quantity,
    decimal UnitPrice,
    decimal Total,
    DateTime QueryDate
);