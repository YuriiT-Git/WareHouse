using WIS.Application.DTO;
using WIS.Domain.Entities;

namespace WIS.Application.Common.Abstractions;

public interface IInventoryItemRepository
{
    Task AddInventoryItemAsync(InventoryItem item, CancellationToken cancellationToken);
    Task<InventoryItem> GetInventoryItemAsync(string code, CancellationToken cancellationToken);
    Task UpdateInventoryStockDataAsync(InventoryItem inventoryItem, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<InventoryItemInfoDto>> GetAllInventoryItemAsync(CancellationToken cancellationToken);
    Task<InventoryItemInfoDto> GetInventoryItemExtendedAsync(string code, CancellationToken cancellationToken);
}