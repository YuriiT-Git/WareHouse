using System.Text.Json;
using WIS.Domain.Abstractions;
using WIS.Domain.ValueObjects.Enums;

namespace WIS.Domain.Events;

public class InventoryItemCreatedEvent : EventBase, IDomainEvent
{
    public required ProductType ProductType { get; init; }
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required ItemSize Size { get; init; }
    public required string Color { get; init; }

    public InventoryItemCreatedEvent()
    {
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public string GetEventData()
    {
        return JsonSerializer.Serialize(this);
    }
    
    public static InventoryItemCreatedEvent? FromJson(string json)
    {
        return JsonSerializer.Deserialize<InventoryItemCreatedEvent>(json);
    }
}