using System.Text.Json;
using WIS.Domain.Abstractions;

namespace WIS.Domain.Events;

public class StockRemovedEvent: EventBase, IDomainEvent
{
    public int Quantity { get; set; }
    
    public static StockRemovedEvent? FromJson(string json)
    {
        return JsonSerializer.Deserialize<StockRemovedEvent>(json);
    }

    public string GetEventData()
    {
        return JsonSerializer.Serialize(this);
    }
}