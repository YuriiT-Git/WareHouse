using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.EventPublisher;
using WIS.Domain.Extensions;

namespace WIS.Application.Features.ReceiveInventoryItem;

public class RegisterIncomingStockHandler(
    IInventoryItemRepository inventoryItemRepository,
    IEventPublisher eventPublisher
    )
    : IRequestHandler<RegisterIncomingStockRequest>
{
    public async Task Handle(RegisterIncomingStockRequest command, CancellationToken cancellationToken)
    {
        var inventoryItem = await inventoryItemRepository.GetInventoryItemAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterIncoming(command.Quantity);
        await inventoryItemRepository.UpdateInventoryStockDataAsync(inventoryItem, cancellationToken);
        await eventPublisher.PublishAsync(inventoryItem.ToStockUpdatedEvent(), cancellationToken);
    }
}