using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;

namespace WIS.Application.Features.ShipInventoryItem;

public class RegisterOutgoingStockHandler(IInventoryItemRepository repository)
    : IRequestHandler<RegisterOutgoingStockRequest>
{
    public async Task Handle(RegisterOutgoingStockRequest command, CancellationToken cancellationToken)
    {
        var inventoryItem = await repository.GeAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterOutgoing(command.Quantity);
        await repository.UpdateStockDataAsync(inventoryItem, cancellationToken);
    }
}