using WIS.Domain.Events;

namespace WIS.Infrastructure.Audit.Entities;

public class InventoryAuditLogDataModel: StockUpdatedEvent
{
    public int Id { get; internal set; }
}