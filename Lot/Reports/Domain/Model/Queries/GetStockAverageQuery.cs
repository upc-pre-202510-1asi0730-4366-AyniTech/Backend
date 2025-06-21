namespace Lot.Reports.Domain.Model.Queries;

public class GetStockAverageQuery
{
    public string Product { get; set; }

    public GetStockAverageQuery(string product)
    {
        Product = product;
    }
}