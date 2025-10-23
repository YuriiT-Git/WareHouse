using Nexus.Abstractions;
using WIS.Application.Commands;
using WIS.Application.Common.Abstractions;

namespace WIS.Application.Handlers;

public class RegisterOutgoingStockCommandHandler(IInventoryItemRepository repository)
    : ICommandHandler<RegisterOutgoingStockCommand, bool>
{
    public async Task<bool> Handle(RegisterOutgoingStockCommand command, CancellationToken cancellationToken)
    {
        //TODO: Hand;e exceptions
        var inventoryItem = await repository.GetInventoryItemAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterOutgoing(command.Quantity);
        await repository.UpdateInventoryStockDataAsync(inventoryItem, cancellationToken);
        return true;
    }
}