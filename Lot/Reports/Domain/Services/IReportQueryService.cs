using Lot.Reports.Domain.Model.Aggregates;

namespace Lot.Reports.Domain.Services;

/// <summary>
/// Report query service interface.
/// </summary>
public interface IReportQueryService
{
    /// <summary>
    /// Devuelve los reportes filtrados por nombre de campo.
    /// </summary>
    Task<IEnumerable<Report>> GetByFieldNameAsync(string fieldName);

    /// <summary>
    /// Devuelve los reportes filtrados por rango de fechas.
    /// </summary>
    Task<IEnumerable<Report>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    /// <summary>
    /// Devuelve todos los reportes creados en el sistema.
    /// </summary>
    Task<IEnumerable<Report>> GetAllAsync();
    
    Task<IEnumerable<Report>> GetReportsByProductAsync();
    Task<IEnumerable<Report>> GetReportsByCategoryAsync();
}