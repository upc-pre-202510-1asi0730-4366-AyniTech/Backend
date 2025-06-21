namespace Lot.Reports.Domain.Model.Aggregates;

public class CategoryReport
{
    public int Id { get; private set; }
    public int ReportId { get; private set; }
    public string Category { get; private set; }
    public string Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Total { get; private set; }
    public DateOnly QueryDate { get; private set; }

    public CategoryReport(int reportId, string category, string product, int quantity, decimal unitPrice, decimal total, DateOnly queryDate)
    {
        ReportId = reportId;
        Category = category;
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Total = total;
        QueryDate = queryDate;
    }

    public Report Report { get; private set; } 
}