using Nexus.Abstractions;
using WIS.Application.Commands;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Entities;

namespace WIS.Application.Handlers;

public class CreateInventoryItemCommandHandler(IInventoryItemRepository inventoryItemRepository)
    : ICommandHandler<CreateInventoryItemCommand, string>
{
    public async Task<string> Handle(CreateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        var inventoryEntity = InventoryItem.Create(
            command.ProductType,
            command.Brand,
            command.Model,
            command.Color,
            command.Size);
        
        await inventoryItemRepository.AddInventoryItemAsync(inventoryEntity, cancellationToken);
        return inventoryEntity.SkuNumber;
    }
}