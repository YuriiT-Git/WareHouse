using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.EventPublisher;
using WIS.Domain.Entities;
using WIS.Domain.Events;
using WIS.Domain.Extensions;
using WIS.Infrastructure.Repositories;

namespace WIS.Application.Features.CreateInventoryItem;

public class CreateInventoryItemCommandHandler(
    IEventStorageRepository eventStorageRepository,
    IEventPublisher eventPublisher,
    IInventoryAuditLogRepository inventoryAuditLogRepository)
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

        var existedInventory =
            await inventoryAuditLogRepository.GetAsync(aggregate.SkuNumber, cancellationToken);

        if (existedInventory is not null)
        {
            throw new InvalidOperationException($"Inventory already exists sku number {aggregate.SkuNumber}");
        }
        
        await aggregate.RaiseEventsAsync(x=>eventStorageRepository.AddAsync(x,cancellationToken));
        await eventPublisher.PublishAsync(aggregate.ToStockUpdatedEvent(), cancellationToken);
        
        return aggregate.SkuNumber;
    }
}