using Microsoft.EntityFrameworkCore;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Events;
using WIS.Infrastructure.Persistence.Entities;
using WIS.Infrastructure.Persistence.Extensions;

namespace WIS.Infrastructure.Persistence.Repositories;

public class InventoryAuditLogRepository(WareHouseDbContext dbContext)
    : IInventoryAuditLogRepository
{
    public async Task AddAsync(StockUpdatedEvent stockUpdatedEvent, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(stockUpdatedEvent);

        var entity = dbContext
            .Set<InventoryAuditLogDataModel>().FirstOrDefault(x => x.Code == stockUpdatedEvent.Code);

        if (entity == null)
        {
            await dbContext.AddAsync(stockUpdatedEvent.ToAggregatedData(), ct);
        }
        else
        {
            entity.Code = stockUpdatedEvent.Code;
            entity.ProductType = stockUpdatedEvent.ProductType;
            entity.Brand = stockUpdatedEvent.Brand;
            entity.Model = stockUpdatedEvent.Model;
            entity.Size = stockUpdatedEvent.Size;
            entity.Color = stockUpdatedEvent.Color;
            entity.Quantity = stockUpdatedEvent.Quantity;
            entity.UpdatedAt = stockUpdatedEvent.UpdatedAt;
        }

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<StockUpdatedEvent> GetAsync(string code, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(code);
        var result = await dbContext
            .Set<InventoryAuditLogDataModel>()
            .FirstOrDefaultAsync(x => x.Code == code, ct);
        return result;
    }

    public async Task<IReadOnlyCollection<StockUpdatedEvent>> GetLowStockItemsAsync(int lowCountBound,
        CancellationToken ct)
    {
        var lowStockItems = await dbContext
            .Set<InventoryAuditLogDataModel>()
            .Where(e => e.Quantity < lowCountBound)
            .ToArrayAsync(ct);
        return lowStockItems;
    }

    public async Task<IReadOnlyCollection<StockUpdatedEvent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext
            .Set<InventoryAuditLogDataModel>()
            .ToArrayAsync(cancellationToken);
    }
}