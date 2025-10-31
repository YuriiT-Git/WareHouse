using Microsoft.EntityFrameworkCore;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Events;
using WIS.Infrastructure.Entities;
using WIS.Infrastructure.Extensions;

namespace WIS.Infrastructure.Repositories;

public class InventoryAuditLogRepository(WareHouseDbContext dbContext)
    : IInventoryAuditLogRepository
{
    public async Task AddAsync(StockUpdatedEvent stockUpdatedEvent, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(stockUpdatedEvent);
        await dbContext.AddAsync(stockUpdatedEvent.ToAggregatedData(), ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<StockUpdatedEvent[]> GetAsync(string code, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(code);
        var result = await dbContext
            .Set<InventoryAuditLogDataModel>()
            .Where(x=>x.Code == code)
            .OrderBy(x=>x.UpdatedAt)
            .Select(x=>x.ToAuditData())
            .ToArrayAsync(ct);
        return result;
    }

    public async Task<StockUpdatedEvent[]> GetLowStockItemsAsync(int lowCountBound, CancellationToken ct)
    {
        var lowStockItems = await dbContext
            .Set<InventoryAuditLogDataModel>()
            .Where(e =>
                e.UpdatedAt ==
                dbContext.Set<InventoryAuditLogDataModel>()
                    .Where(x => x.Code == e.Code)
                    .Max(x => x.UpdatedAt)
                && e.Quantity < lowCountBound)
            .ToArrayAsync(ct);
        return lowStockItems;
    }
}