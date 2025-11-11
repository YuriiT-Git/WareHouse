using WIS.Application.Common.Common.DTO;
using WIS.Domain.Entities;

namespace WIS.Application.Common.Extensions;

public static class InventoryAggregateExtensions
{
        
    public static InventoryItemDto? ToInventoryItem(this InventoryItemAggregate aggregate)
    {
        return new InventoryItemDto
        {
            Quantity = aggregate.InventoryStock.Quantity,
            SkuNumber = aggregate.SkuNumber
        };
    }
}