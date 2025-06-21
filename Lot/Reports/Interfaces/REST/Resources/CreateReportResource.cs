namespace Lot.Reports.Interfaces.REST.Resources;

public class CreateReportResource
{
    public string FieldName { get; set; }
    public DateTime FilterStartDate { get; set; }
    public DateTime FilterEndDate { get; set; }
}