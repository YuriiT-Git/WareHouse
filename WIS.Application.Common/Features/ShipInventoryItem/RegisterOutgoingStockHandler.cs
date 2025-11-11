using MedistR.Abstractions;
using WIS.Application.Common.Common.Abstractions;
using WIS.Application.Common.EventPublisher;
using WIS.Domain.Extensions;

namespace WIS.Application.Common.Features.ShipInventoryItem;

public class RegisterOutgoingStockHandler(IEventStorageRepository eventStorageRepository,
    IEventPublisher eventPublisher)
    : IRequestHandler<RegisterOutgoingStockRequest>
{
    public async Task Handle(RegisterOutgoingStockRequest command, CancellationToken cancellationToken)
    {
        var inventoryItem = await eventStorageRepository.GetAsync(command.Code, cancellationToken);
        inventoryItem.RegisterOutgoingStock(command.Quantity);
        await eventPublisher.PublishAsync(inventoryItem.ToStockUpdatedEvent(), cancellationToken);
        await inventoryItem.RaiseEventsAsync(x=>eventStorageRepository.AddAsync(x, cancellationToken));
    }
}