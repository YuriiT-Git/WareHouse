using WIS.Domain.Events;

namespace WIS.Application.AuditService;

public interface IInventoryAuditLogRepository
{
    public Task AddAsync(StockUpdatedEvent item, CancellationToken ct);
    public Task<StockUpdatedEvent> GetAsync(string code, CancellationToken ct );

    public Task<IReadOnlyCollection<StockUpdatedEvent>> GetLowStockItemsAsync(int lowCountBound, CancellationToken ct);
    Task<IReadOnlyCollection<StockUpdatedEvent>> GetAllAsync(CancellationToken cancellationToken);
}