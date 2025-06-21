
namespace Lot.Reports.Domain.Model.Queries;

public class GetReportsByDateQuery
{
    public DateOnly QueryDate { get; set; }

    public GetReportsByDateQuery(DateOnly queryDate)
    {
        QueryDate = queryDate;
    }
}