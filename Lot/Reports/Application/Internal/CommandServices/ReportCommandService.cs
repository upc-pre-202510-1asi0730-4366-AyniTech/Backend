using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Domain.Model.Commands;
using Lot.Reports.Domain.Repositories;
using Lot.Reports.Domain.Services;

namespace Lot.Reports.Application.Internal.CommandServices;

public class ReportCommandService : IReportCommandService
{
    private readonly IReportRepository _repository;

    public ReportCommandService(IReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> CreateAsync(CreateReportCommand command)
    {
        var report = new Report(command);
        await _repository.AddAsync(report);
        return "Reporte generado correctamente";
    }
}