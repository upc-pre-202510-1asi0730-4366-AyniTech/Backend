namespace Lot.Reports.Interfaces.REST.Resources;

public class StockAverageResource
{
    public int Id { get; set; }
    public string Category { get; set; }
    public string Product { get; set; }
    public double AverageQuantity { get; set; }
    public double IdealStock { get; set; }
    public DateTime QueryDate { get; set; }
    public string Status { get; set; }

    public StockAverageResource(int id, string category, string product, double averageQuantity, double idealStock, DateTime queryDate, string status)
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