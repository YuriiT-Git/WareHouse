using Microsoft.EntityFrameworkCore;
using WIS.Application.AuditService;
using WIS.Application.AuditService.DTO;
using WIS.Domain.Events;
using WIS.Infrastructure.Audit.Entities;
using WIS.Infrastructure.Audit.Extensions;

namespace WIS.Infrastructure.Audit.Repositories;

public class InventoryAuditLogRepository(AuditDbContext dbContext)
    : IInventoryAuditLogRepository
{
    public async Task AddAsync(StockUpdatedEvent stockUpdatedEvent, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(stockUpdatedEvent);

        var entity = dbContext
            .Set<InventoryAuditLogDataModel>().FirstOrDefault(x => x.Code == stockUpdatedEvent.Code);

        if (entity == null)
        {
            await dbContext.AddAsync(stockUpdatedEvent.ToDbModel(), ct);
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

    public async Task<AuditLogDto> GetAsync(string code, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(code);
        var result = await dbContext
            .Set<InventoryAuditLogDataModel>()
            .FirstOrDefaultAsync(x => x.Code == code, ct);
        return result.ToAuditLog();
    }

    public async Task<IReadOnlyCollection<AuditLogDto>> GetLowStockItemsAsync(int lowCountBound,
        CancellationToken ct)
    {
        var lowStockItems = await dbContext
            .Set<InventoryAuditLogDataModel>()
            .Where(e => e.Quantity < lowCountBound)
            .ToArrayAsync(ct);
        return lowStockItems.Select(x => x.ToAuditLog()).ToArray();
    }

    public async Task<IReadOnlyCollection<AuditLogDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var allItems = await dbContext
            .Set<InventoryAuditLogDataModel>()
            .ToArrayAsync(cancellationToken);
        var result = allItems.Select(x => x.ToAuditLog());

        return result.ToArray();
    }
}