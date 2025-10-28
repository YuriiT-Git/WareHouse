using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Entities;

namespace WIS.Application.Features.CreateInventoryItem;

public class CreateInventoryItemCommandHandler(IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<CreateInventoryItemCommand, string>
{
    public async Task<string> Handle(CreateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        var inventoryEntity = InventoryItem.Create(
            command.ProductType,
            command.Brand,
            command.Model,
            command.Color,
            command.Size);
        
        await inventoryItemRepository.AddAsync(inventoryEntity, cancellationToken);
        return inventoryEntity.SkuNumber;
    }
}