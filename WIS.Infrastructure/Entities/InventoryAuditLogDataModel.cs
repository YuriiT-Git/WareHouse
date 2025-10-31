using WIS.Domain.Events;

namespace WIS.Infrastructure.Entities;

public class InventoryAuditLogDataModel: StockUpdatedEvent
{
    public int Id { get; internal set; }
}