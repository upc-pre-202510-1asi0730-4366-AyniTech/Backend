using Lot.Reports.Domain.Model.Commands;

namespace Lot.Reports.Domain.Services;

public interface IReportCommandService
{
    Task<string> CreateAsync(CreateReportCommand command);
}