using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.EventPublisher;
using WIS.Domain.Extensions;

namespace WIS.Application.Features.ShipInventoryItem;

public class RegisterOutgoingStockHandler(IInventoryItemRepository repository,
    IEventPublisher eventPublisher)
    : IRequestHandler<RegisterOutgoingStockRequest>
{
    public async Task Handle(RegisterOutgoingStockRequest command, CancellationToken cancellationToken)
    {
        var inventoryItem = await repository.GeAsync(command.Code, cancellationToken);
        inventoryItem.InventoryStock.RegisterOutgoing(command.Quantity);
        await repository.UpdateStockDataAsync(inventoryItem, cancellationToken);
        await eventPublisher.PublishAsync(inventoryItem.ToStockUpdatedEvent(), cancellationToken);
    }
}