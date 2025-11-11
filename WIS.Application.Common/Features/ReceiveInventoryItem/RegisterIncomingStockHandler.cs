using MedistR.Abstractions;
using WIS.Application.Common.Common.Abstractions;
using WIS.Application.Common.EventPublisher;
using WIS.Domain.Extensions;

namespace WIS.Application.Common.Features.ReceiveInventoryItem;

public class RegisterIncomingStockHandler(
    IEventStorageRepository eventStorageRepository,
    IEventPublisher eventPublisher)
    : IRequestHandler<RegisterIncomingStockRequest>
{
    public async Task Handle(RegisterIncomingStockRequest command, CancellationToken cancellationToken)
    {
        var inventoryItemAggregate = await eventStorageRepository.GetAsync(command.Code, cancellationToken);
        inventoryItemAggregate.RegisterIncomingStock(command.Quantity);
        await eventPublisher.PublishAsync(inventoryItemAggregate.ToStockUpdatedEvent(), cancellationToken);
        await inventoryItemAggregate.RaiseEventsAsync(x=>eventStorageRepository.AddAsync(x, cancellationToken));
    }
}