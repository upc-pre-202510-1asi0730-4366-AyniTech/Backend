namespace Lot.Inventaries.Domain.Model.Queries;

/// <summary>
/// Get All Lot By Price Query 
/// </summary>
/// <param name="Price">
/// The  unit price to search for.
/// </param>

public record GetAllLotByPrice(decimal Price);