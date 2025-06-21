namespace Lot.Reports.Domain.Model.Commands;

public class CreateReportCommand
{
    public string FieldName { get; set; }
    public DateTime FilterStartDate { get; set; }
    public DateTime FilterEndDate { get; set; }

    public CreateReportCommand(string fieldName, DateTime filterStartDate, DateTime filterEndDate)
    {
        FieldName = fieldName;
        FilterStartDate = filterStartDate;
        FilterEndDate = filterEndDate;
    }
}