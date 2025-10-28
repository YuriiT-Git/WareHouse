using Microsoft.EntityFrameworkCore;
using WIS.Application.Common.Abstractions;
using WIS.Application.DTO;
using WIS.Domain.Entities;
using WIS.Domain.Exceptions;
using WIS.Infrastructure.Extensions;

namespace WIS.Infrastructure.Repositories;

public class InventoryItemRepository(WareHouseDbContext dbContext) : IInventoryItemRepository
{
    public async Task AddAsync(InventoryItem item, CancellationToken cancellationToken)
    {
        
        try
        {
            var entity = item.ToDbModel();
            await dbContext.InventoryItems.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.Message.Contains("duplicate"))
        {
            throw new DuplicateInventoryItemException(item.SkuNumber);
        }
    }

    public async Task<InventoryItem> GeAsync(string code, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext
            .InventoryItems.Where(x => x.Code == code)
            .Include(x => x.InventoryStock)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (dbEntity is null)
        {
            throw new InventoryNotFoundException(code);
        }

        var inventoryEntity = dbEntity.ToDomain();
        return inventoryEntity;
    }
    
    public async Task<InventoryItemDto> GetInfoAsync(string code, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext
            .InventoryItems.Where(x => x.Code == code)
            .Include(x => x.InventoryStock)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (dbEntity is null)
        {
            throw new InventoryNotFoundException(code);
        }

        return dbEntity.ToDto();
    }

    public async Task<IReadOnlyCollection<InventoryItemDto>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        var dbEntities = await dbContext
            .InventoryItems
            .Include(x => x.InventoryStock)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        return dbEntities
            .Select(x => x.ToDto())
            .ToList();
    }

    public async Task UpdateStockDataAsync(InventoryItem inventoryItem, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext
            .InventoryItems
            .Include(x => x.InventoryStock)
            .FirstOrDefaultAsync(x => x.Code == inventoryItem.SkuNumber, cancellationToken);

        if (dbEntity is null)
        {
            throw new InventoryNotFoundException(inventoryItem.SkuNumber);
        }
        
        dbEntity.InventoryStock = inventoryItem.InventoryStock.ToUpdateDbModel(dbEntity.InventoryStock!);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

