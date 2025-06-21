using Lot.Reports.Domain.Model.Commands;
using Lot.Reports.Domain.Model.ValueObjects;

namespace Lot.Reports.Domain.Model.Aggregates;

/// completado

public partial class Report
{
    public int Id { get; }
    public ReportField Field { get; private set; }
    public ReportFilter Filter { get; private set; }

    public string FieldName => Field.Name;
    public DateTime FilterStartDate => Filter.StartDate;
    public DateTime FilterEndDate => Filter.EndDate;

    public Report()
    {
        Field = new ReportField();
        Filter = new ReportFilter();
    }

    public Report(string fieldName, DateTime startDate, DateTime endDate)
    {
        Field = new ReportField(fieldName);
        Filter = new ReportFilter(startDate, endDate);
    }

    public Report(CreateReportCommand command)
    {
        Field = new ReportField(command.FieldName);
        Filter = new ReportFilter(command.FilterStartDate, command.FilterEndDate);
    }
}