using MedistR.Abstractions;
using WIS.Application.EventPublisher;
using WIS.Domain.Extensions;
using WIS.Infrastructure.Repositories;

namespace WIS.Application.Features.ReceiveInventoryItem;

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