using WIS.Application.AuditService.DTO;
using WIS.Domain.Events;

namespace WIS.Application.AuditService;

public interface IInventoryAuditLogRepository
{
    public Task AddAsync(StockUpdatedEvent stockUpdatedEvent, CancellationToken ct);
    public Task<AuditLogDto> GetAsync(string code, CancellationToken ct );
    public Task<IReadOnlyCollection<AuditLogDto>> GetLowStockItemsAsync(int lowCountBound, CancellationToken ct);
    Task<IReadOnlyCollection<AuditLogDto>> GetAllAsync(CancellationToken cancellationToken);
}