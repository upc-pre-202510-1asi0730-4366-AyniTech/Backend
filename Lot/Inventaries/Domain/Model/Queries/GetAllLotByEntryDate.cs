namespace Lot.Inventaries.Domain.Model.Queries;

/// <summary>
/// Get All Lot By Entry Date Query 
/// </summary>
/// <param name="StartDate">
/// The start date for the entry date range to search.
/// </param>

public record GetAllLotByEntryDate(DateTime StartDate);