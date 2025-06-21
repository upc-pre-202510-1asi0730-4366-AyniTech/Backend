using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Domain.Repositories;
using Lot.Reports.Domain.Services;


namespace Lot.Reports.Application.Internal.QueryServices;

public class ReportQueryService : IReportQueryService
{
    private readonly IReportRepository _repository;

    public ReportQueryService(IReportRepository repository)
    {
        _repository = repository;
        
    }

    public async Task<IEnumerable<Report>> GetAllAsync()
    {
        return await _repository.ListAsync();
    }
    
    
    public async Task<IEnumerable<Report>> GetByFieldNameAsync(string fieldName)
    {
        var all = await _repository.ListAsync();
        return all.Where(r => r.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Report>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var all = await _repository.ListAsync();
        return all.Where(r => r.FilterStartDate >= startDate && r.FilterEndDate <= endDate);
    }
    
    public async Task<IEnumerable<Report>> GetReportsByProductAsync()
    {
        return await _repository.ListByProductAsync();
    }
    
    public async Task<IEnumerable<Report>> GetReportsByCategoryAsync()
    {
        return await _repository.ListByCategoryAsync(); // Asegúrate de que este método exista también en el repositorio
    }

}