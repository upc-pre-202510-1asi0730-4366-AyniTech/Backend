namespace Lot.Reports.Domain.Model.Queries;

public class GetReportsByCategoryQuery
{
    public string Category { get; set; }

    public GetReportsByCategoryQuery(string category)
    {
        Category = category;
    }
}