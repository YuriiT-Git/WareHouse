namespace WIS.Domain.Events;

public abstract class EventBase
{
    public required DateTimeOffset CreatedAt { get; init; }
    public required string SkuNumber { get; set; }
    public string GetTypeName() => GetType().Name;
}