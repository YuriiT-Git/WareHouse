using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;

namespace WIS.Application.Features.ShipInventoryItem;

public class RegisterOutgoingStockCommandHandler(IInventoryItemRepository repository)
    : ICommandHandler<RegisterOutgoingStockCommand>
{
    public async Task Handle(RegisterOutgoingStockCommand command, CancellationToken cancellationToken)
    {
        //TODO: Hand;e exceptions
        var inventoryItem = await repository.GetInventoryItemAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterOutgoing(command.Quantity);
        await repository.UpdateInventoryStockDataAsync(inventoryItem, cancellationToken);
    }
}