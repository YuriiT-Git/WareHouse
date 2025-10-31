using WIS.Domain.Entities;
using WIS.Domain.Events;

namespace WIS.Domain.Extensions;

public static class InventoryItemExtensions
{
    public static StockUpdatedEvent ToStockUpdatedEvent(this InventoryItemAggregate inventoryItemAggregate)
    {
        return new StockUpdatedEvent
        {
            Brand = inventoryItemAggregate.Brand,
            Model = inventoryItemAggregate.Model,
            Code = inventoryItemAggregate.SkuNumber,
            Color = inventoryItemAggregate.Color,
            ProductType = inventoryItemAggregate.ProductType,
            Size = inventoryItemAggregate.Size,
            Quantity = inventoryItemAggregate.InventoryStock.Quantity,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }
}