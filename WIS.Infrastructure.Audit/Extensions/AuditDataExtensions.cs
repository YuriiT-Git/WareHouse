using WIS.Domain.Events;
using WIS.Infrastructure.Audit.Entities;

namespace WIS.Infrastructure.Audit.Extensions;

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
}