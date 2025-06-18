

using Lot.Inventaries.Domain.Model.ValueOjbects;

namespace Lot.Inventaries.Domain.Model.Queries; 

/// <summary>
/// Get All Lot By Product Query 
/// </summary>
/// <param name="Product">
/// The <see cref="ProductName"/> of the product to search for in inventory.
/// </param>
public record GetAllLotByProduct(ProductName Product);