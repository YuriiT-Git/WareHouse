using System.Text.Json;
using WIS.Domain.Abstractions;

namespace WIS.Domain.Events;

public class StockAddedEvent: EventBase, IDomainEvent
{
    public int Quantity { get; set; }
    
    public static StockAddedEvent? FromJson(string json)
    {
        return JsonSerializer.Deserialize<StockAddedEvent>(json);
    }
    
    public string GetEventData()
    {
        return JsonSerializer.Serialize(this);
    }
}