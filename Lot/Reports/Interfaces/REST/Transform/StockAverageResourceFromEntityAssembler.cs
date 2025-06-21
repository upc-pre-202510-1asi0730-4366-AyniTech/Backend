using Lot.Reports.Domain.Model.Aggregates;
using Lot.Reports.Interfaces.REST.Resources;

namespace Lot.Reports.Interfaces.REST.Transform;

public static class StockAverageResourceFromEntityAssembler
{
    public static StockAverageResource ToResource(StockAverageReport entity)
    {
        return new StockAverageResource(
            entity.Id,
            entity.Category,
            entity.Product,
            entity.AverageQuantity,
            entity.IdealStock,
            entity.QueryDate,
            entity.Status
        );
    }
}