namespace Lot.Reports.Domain.Model.Queries;

public class GetReportsByProductQuery
{
    public string Product { get; set; }

    public GetReportsByProductQuery(string product)
    {
        Product = product;
    }
}