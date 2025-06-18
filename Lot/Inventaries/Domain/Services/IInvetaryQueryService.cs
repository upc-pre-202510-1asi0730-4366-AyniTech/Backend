
using Lot.Inventaries.Domain.Model.Aggregates;  
using Lot.Inventaries.Domain.Model.Queries;     
using Lot.Inventaries.Domain.Model.ValueOjbects; 
namespace Lot.Inventaries.Domain.Services; 

/// <summary>
/// Inventory query service interface 
/// </summary>
public interface IInvetaryQueryService 
{


    /// <summary>
    /// Handles the get all lot by entry date query.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllLotByEntryDate"/> query.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> objects matching the entry date.
    /// </returns>
    Task<IEnumerable<Inventary>> Handle(GetAllLotByEntryDate query);

    /// <summary>
    /// Handles the get all lot by price query.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllLotByPrice"/> query.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> objects matching the unit price.
    /// </returns>
    Task<IEnumerable<Inventary>> Handle(GetAllLotByPrice query);

    /// <summary>
    /// Handles the get all lot by product query.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllLotByProduct"/> query.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> objects matching the product.
    /// </returns>
    Task<IEnumerable<Inventary>> Handle(GetAllLotByProduct query);

    /// <summary>
    /// Handles the get all lot by supplier query.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllLotBySupplier"/> query.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> objects matching the supplier.
    /// </returns>
    Task<IEnumerable<Inventary>> Handle(GetAllLotBySupplier query);

    /// <summary>
    /// Handles the get all lot by quantity query.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAlLotByQuantity"/> query.
    /// </param>
    /// <returns>
    /// A list of <see cref="Inventary"/> objects matching the quantity.
    /// </returns>
    Task<IEnumerable<Inventary>> Handle(GetAlLotByQuantity query);
}