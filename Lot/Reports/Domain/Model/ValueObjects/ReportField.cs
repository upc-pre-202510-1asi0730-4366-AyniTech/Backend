namespace Lot.Reports.Domain.Model.ValueObjects;


public class ReportField
{
    public string Name { get; private set; }

    public ReportField()
    {
        Name = string.Empty;
    }

    public ReportField(string name)
    {
        Name = name;
    }
}