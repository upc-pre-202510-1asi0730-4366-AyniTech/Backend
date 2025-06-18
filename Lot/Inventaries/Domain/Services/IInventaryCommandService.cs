using Lot.Inventaries.Domain.Model.Aggregates;
using Lot.Inventaries.Domain.Model.Commands;  
namespace Lot.Inventaries.Domain.Services; 
/// <summary>
/// Inventory command service interface 
/// </summary>
public interface IInventaryCommandService
{
    /// <summary>
    /// Handles the create inventory command.
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreateInventaryCommand"/> command.
    /// </param>
    /// <returns>
    /// The <see cref="Inventary"/> object with the created inventory record, or null if creation failed.
    /// </returns>
    Task<Inventary?> Handle(CreateInventaryCommand command);


}