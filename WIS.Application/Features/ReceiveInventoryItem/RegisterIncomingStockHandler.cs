using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;

namespace WIS.Application.Features.ReceiveInventoryItem;

public class RegisterIncomingStockHandler(
    IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<RegisterIncomingStockRequest>
{
    public async Task Handle(RegisterIncomingStockRequest command, CancellationToken cancellationToken)
    {
        //TODO: Handle exceptions
        
        var inventoryItem = await inventoryItemRepository.GetInventoryItemAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterIncoming(command.Quantity);
        await inventoryItemRepository.UpdateInventoryStockDataAsync(inventoryItem, cancellationToken);
    }
}