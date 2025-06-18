using Lot.Inventaries.Domain.Model.ValueOjbects;
namespace Lot.Inventaries.Domain.Model.Queries; 
/// <summary>
/// Get All Lot By Supplier Query 
/// </summary>
/// <param name="Supplier">
/// The <see cref="Supplier"/> of the product to search for in inventory.
/// </param>
public record GetAllLotBySupplier(Supplier Supplier);