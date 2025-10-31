namespace WIS.Domain.Abstractions;

public interface IDomainEvent
{
    public string SkuNumber { get; internal set; }
    public DateTimeOffset CreatedAt { get; init; }
    public string GetEventData();
    public string GetTypeName(); 
}