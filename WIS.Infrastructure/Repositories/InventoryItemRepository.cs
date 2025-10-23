using Microsoft.EntityFrameworkCore;
using WIS.Application.Common.Abstractions;
using WIS.Application.DTO;
using WIS.Domain.Entities;
using WIS.Infrastructure.Extensions;

namespace WIS.Infrastructure.Repositories;

public class InventoryItemRepository(WareHouseDbContext dbContext) : IInventoryItemRepository
{
    public async Task AddInventoryItemAsync(InventoryItem item, CancellationToken cancellationToken)
    {
        var entity = item.ToDbModel();
        await dbContext.InventoryItems.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<InventoryItem> GetInventoryItemAsync(string code, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext
            .InventoryItems.Where(x => x.Code == code)
            .Include(x => x.InventoryStock)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        //TODO: add verifications and handle exceptions


        var inventoryEntity = dbEntity.ToDomain();
        return inventoryEntity;
    }
    
    public async Task<InventoryItemInfoDto> GetInventoryItemExtendedAsync(string code, CancellationToken cancellationToken)
    {
        var dbEntity = await dbContext
            .InventoryItems.Where(x => x.Code == code)
            .Include(x => x.InventoryStock)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        //TODO: add verifications and handle exceptions

        return dbEntity.ToDto();
    }

    public async Task<IReadOnlyCollection<InventoryItemInfoDto>> GetAllInventoryItemAsync(
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

    public async Task UpdateInventoryStockDataAsync(InventoryItem inventoryItem, CancellationToken cancellationToken)
    {
        var dbEntity = dbContext
            .InventoryItems
            .Include(x => x.InventoryStock)
            .FirstOrDefault(x => x.Code == inventoryItem.SkuNumber);

        dbEntity.InventoryStock = inventoryItem.InventoryStock.ToUpdateDbModel(dbEntity.InventoryStock);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}