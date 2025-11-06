using MedistR.Abstractions;
using WIS.Application.Common.EventPublisher;
using WIS.Domain.Entities;
using WIS.Domain.Events;
using WIS.Domain.Extensions;
using WIS.Infrastructure.Repositories;

namespace WIS.Application.Common.Features.CreateInventoryItem;

public class CreateInventoryItemCommandHandler(
    IEventStorageRepository eventStorageRepository,
    IEventPublisher eventPublisher)
    : IRequestHandler<CreateInventoryItemCommand, string>
{
    public async Task<string> Handle(CreateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        var @event = new InventoryItemCreatedEvent
        {
            ProductType = command.ProductType,
            Brand = command.Brand,
            Model = command.Model,
            Color = command.Color,
            Size = command.Size,
            CreatedAt = DateTimeOffset.UtcNow,
            SkuNumber = ""
        };
        
        var aggregate = InventoryItemAggregate.Create(@event);

       
        await aggregate.RaiseEventsAsync(x=>eventStorageRepository.AddAsync(x,cancellationToken));
        await eventPublisher.PublishAsync(aggregate.ToStockUpdatedEvent(), cancellationToken);
        
        return aggregate.SkuNumber;
    }
}