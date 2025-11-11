namespace WIS.Infrastructure.Persistence.Entities;

public class EventsStorageModel
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string EventType { get; set; }
    public required string Data { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
}