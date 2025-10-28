using WIS.Application.DTO;
using WIS.Domain.Entities;

namespace WIS.Application.Common.Abstractions;

public interface IInventoryItemRepository
{
    Task AddAsync(InventoryItem item, CancellationToken cancellationToken);
    Task<InventoryItem> GeAsync(string code, CancellationToken cancellationToken);
    Task UpdateStockDataAsync(InventoryItem inventoryItem, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<InventoryItemDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<InventoryItemDto> GetInfoAsync(string code, CancellationToken cancellationToken);
}