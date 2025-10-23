using WIS.Domain.Entities;

namespace WIS.Domain.ValueObjects;

public record InventoryItemInfo(InventoryItem Item, int Count)
{
}