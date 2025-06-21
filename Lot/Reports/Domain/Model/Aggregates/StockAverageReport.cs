namespace Lot.Reports.Domain.Model.Aggregates;

public class StockAverageReport
{
    public int Id { get; private set; }
    public string Category { get; private set; }
    public string Product { get; private set; }
    public double AverageQuantity { get; private set; }
    public double IdealStock { get; private set; }
    public DateTime QueryDate { get; private set; }
    public string Status { get; private set; }

    public StockAverageReport(int id, string category, string product, double averageQuantity, double idealStock, DateTime queryDate, string status)
    {
        Id = id;
        Category = category;
        Product = product;
        AverageQuantity = averageQuantity;
        IdealStock = idealStock;
        QueryDate = queryDate;
        Status = status;
    }
}