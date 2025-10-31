using WIS.Domain.Abstractions;
using WIS.Domain.Events;
using WIS.Infrastructure.Entities;

namespace WIS.Infrastructure.Extensions;

public static class EventsStorageExtensions
{
    public static EventsStorageModel ToDbModel(this IDomainEvent @event)
    {
        return new EventsStorageModel
        {
            Code = @event.SkuNumber,
            EventType = @event.GetTypeName(),

            Data = @event.GetEventData(),
            CreatedAt = @event.CreatedAt
        };
    }
    
    public static IDomainEvent? ToDomain(this EventsStorageModel model)
    {
        return model.EventType switch
        {
            nameof(InventoryItemCreatedEvent) => InventoryItemCreatedEvent.FromJson(model.Data),
            nameof(StockAddedEvent) => StockAddedEvent.FromJson(model.Data),
            nameof(StockRemovedEvent) => StockRemovedEvent.FromJson(model.Data),
            _ => throw new NotImplementedException(model.EventType)
        };
    }
}