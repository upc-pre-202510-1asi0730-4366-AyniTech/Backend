namespace Lot.Inventaries.Domain.Model.Queries;

/// <summary>
/// Get All Lot By Quantity Query 
/// </summary>
/// <param name="Quantity">
/// The  quantity to search for.
/// </param>
public record GetAlLotByQuantity(int Quantity);