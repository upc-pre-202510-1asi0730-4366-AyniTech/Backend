

using Lot.Inventaries.Domain.Model.Aggregates; 
using Lot.Inventaries.Domain.Model.ValueOjbects; 
using Lot.Shared.Domain.Repositories;

namespace Lot.Inventaries.Domain.Repositories; 

/// <summary>
/// Inventory repository interface 
/// </summary>
public interface IInventaryRepository : IBaseRepository<Inventary>
{
    /// <summary>
    /// Finds an inventory record by its product name.
    /// </summary>
    /// <param name="productName">
    /// The <see cref="ProductName"/> of the product to search for.
    /// </param>
    /// <returns>
    /// The <see cref="Inventary"/> if found, otherwise null.
    /// </returns>
    Task<Inventary?> FindInventaryByProductNameAsync(ProductName productName);

    /// <summary>
    /// Finds all inventory records.
    /// </summary>
    /// <returns>
    /// A list of <see cref="Inventary"/> records.
    /// </returns>
    Task<IEnumerable<Inventary>> FindAllInventariesAsync();

    /// <summary>
    /// Finds all inventory records by entry date.
    /// </summary>
    /// <param name="startDate">
    /// The start date to search for.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> records matching the entry date.
    /// </returns>
    Task<IEnumerable<Inventary>> GetAllLotByEntryDate(DateTime startDate);

    /// <summary>
    /// Finds all inventory records by unit price.
    /// </summary>
    /// <param name="price">
    /// The unit price to search for.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> records matching the unit price.
    /// </returns>
    Task<IEnumerable<Inventary>> GetAllLotByPrice(decimal price);

    /// <summary>
    /// Finds all inventory records by product.
    /// </summary>
    /// <param name="product">
    /// The <see cref="ProductName"/> of the product to search for.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> records matching the product.
    /// </returns>
    Task<IEnumerable<Inventary>> GetAllLotByProduct(ProductName product);

    /// <summary>
    /// Finds all inventory records by supplier.
    /// </summary>
    /// <param name="supplier">
    /// The <see cref="Supplier"/> of the product to search for.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> records matching the supplier.
    /// </returns>
    Task<IEnumerable<Inventary>> GetAllLotBySupplier(Supplier supplier);

    /// <summary>
    /// Finds all inventory records by quantity.
    /// </summary>
    /// <param name="quantity">
    /// The quantity to search for.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> records matching the quantity.
    /// </returns>
    Task<IEnumerable<Inventary>> GetAlLotByQuantity(int quantity);
}