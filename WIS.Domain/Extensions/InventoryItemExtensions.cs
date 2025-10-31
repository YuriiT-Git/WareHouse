using WIS.Domain.Entities;
using WIS.Domain.Events;

namespace WIS.Domain.Extensions;

public static class InventoryItemExtensions
{
    public static StockUpdatedEvent ToStockUpdatedEvent(this InventoryItem inventoryItem)
    {
        return new StockUpdatedEvent
        {
            Brand = inventoryItem.Brand,
            Model = inventoryItem.Model,
            Code = inventoryItem.SkuNumber,
            Color = inventoryItem.Color,
            ProductType = inventoryItem.ProductType,
            Size = inventoryItem.Size,
            Quantity = inventoryItem.InventoryStock.Quantity,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }
}