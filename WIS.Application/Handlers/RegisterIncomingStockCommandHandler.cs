using Nexus.Abstractions;
using WIS.Application.Commands;
using WIS.Application.Common.Abstractions;

namespace WIS.Application.Handlers;

public class RegisterIncomingStockCommandHandler(
    IInventoryItemRepository inventoryItemRepository)
    : ICommandHandler<RegisterIncomingStockCommand, bool>
{
    public async Task<bool> Handle(RegisterIncomingStockCommand command, CancellationToken cancellationToken)
    {
        //TODO: Handle exceptions
        
        var inventoryItem = await inventoryItemRepository.GetInventoryItemAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterIncoming(command.Quantity);
        await inventoryItemRepository.UpdateInventoryStockDataAsync(inventoryItem, cancellationToken);
        return true;
    }
}