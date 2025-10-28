using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;

namespace WIS.Application.Features.ReceiveInventoryItem;

public class RegisterIncomingStockHandler(
    IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<RegisterIncomingStockRequest>
{
    public async Task Handle(RegisterIncomingStockRequest command, CancellationToken cancellationToken)
    {
        var inventoryItem = await inventoryItemRepository.GeAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterIncoming(command.Quantity);
        await inventoryItemRepository.UpdateStockDataAsync(inventoryItem, cancellationToken);
    }
}