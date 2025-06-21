namespace Lot.Reports.Domain.Model.ValueObjects;


public class ReportFilter
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public ReportFilter()
    {
        StartDate = DateTime.UtcNow;
        EndDate = DateTime.UtcNow;
    }

    public ReportFilter(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}