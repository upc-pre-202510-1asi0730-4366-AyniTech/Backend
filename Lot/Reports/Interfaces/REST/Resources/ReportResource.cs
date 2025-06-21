namespace Lot.Reports.Interfaces.REST.Resources;

public class ReportResource
{
    public int Id { get; set; }
    public string FieldName { get; set; }
    public DateTime FilterStartDate { get; set; }
    public DateTime FilterEndDate { get; set; }

    public ReportResource(int id, string fieldName, DateTime startDate, DateTime endDate)
    {
        Id = id;
        FieldName = fieldName;
        FilterStartDate = startDate;
        FilterEndDate = endDate;
    }
}