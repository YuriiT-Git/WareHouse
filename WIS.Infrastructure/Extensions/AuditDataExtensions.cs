using WIS.Domain.Events;
using WIS.Infrastructure.Entities;

namespace WIS.Infrastructure.Extensions;

public static class AuditDataExtensions
{
    public static InventoryAuditLogDataModel ToAggregatedData(this StockUpdatedEvent stockUpdatedEvent)
    {
        return new InventoryAuditLogDataModel
        {
            Brand = stockUpdatedEvent.Brand,
            Model = stockUpdatedEvent.Model,
            Code = stockUpdatedEvent.Code,
            Color = stockUpdatedEvent.Color,
            ProductType = stockUpdatedEvent.ProductType,
            Size = stockUpdatedEvent.Size,
            Quantity = stockUpdatedEvent.Quantity,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }

    public static StockUpdatedEvent ToAuditData(this InventoryAuditLogDataModel model)
    {
        return new StockUpdatedEvent
        {
            Brand = model.Brand,
            Model = model.Model,
            Code = model.Code,
            Color = model.Color,
            ProductType = model.ProductType,
            Size = model.Size,
            Quantity = model.Quantity,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }
}