using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;

namespace WIS.Application.Features.ReceiveInventoryItem;

public class RegisterIncomingStockCommandHandler(
    IInventoryItemRepository inventoryItemRepository)
    : ICommandHandler<RegisterIncomingStockCommand>
{
    public async Task Handle(RegisterIncomingStockCommand command, CancellationToken cancellationToken)
    {
        //TODO: Handle exceptions
        
        var inventoryItem = await inventoryItemRepository.GetInventoryItemAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterIncoming(command.Quantity);
        await inventoryItemRepository.UpdateInventoryStockDataAsync(inventoryItem, cancellationToken);
    }
}