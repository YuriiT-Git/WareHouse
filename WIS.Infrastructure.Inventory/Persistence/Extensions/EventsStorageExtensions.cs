using WIS.Application.Common.Common.DTO;
using WIS.Domain.Abstractions;
using WIS.Domain.Events;
using WIS.Infrastructure.Persistence.Entities;

namespace WIS.Infrastructure.Persistence.Extensions;

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
        IDomainEvent? @event = null;
        return model.EventType switch
        {
            nameof(InventoryItemCreatedEvent) => InventoryItemCreatedEvent.FromJson(model.Data),
            nameof(StockAddedEvent) => StockAddedEvent.FromJson(model.Data),
            nameof(StockRemovedEvent) => StockRemovedEvent.FromJson(model.Data),
            _ => throw new NotImplementedException(model.EventType)
        };
    }
    
    public static InventoryItemDto? ToInventoryItem(this EventsStorageModel model)
    {
        IDomainEvent? @event = null;

        switch (model.EventType)
        {
            case nameof(InventoryItemCreatedEvent):
                @event = InventoryItemCreatedEvent.FromJson(model.Data);
                break;
            case nameof(StockAddedEvent):
                @event = StockAddedEvent.FromJson(model.Data);
                break;
            case nameof(StockRemovedEvent):
                @event = StockRemovedEvent.FromJson(model.Data);
                break;
            default:
                throw new NotImplementedException(model.EventType);
        }

        return new InventoryItemDto
        {
            Quantity = @event.Quantity,
            SkuNumber = @event.SkuNumber
        };
    }
}