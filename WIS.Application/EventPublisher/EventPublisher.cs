using MedistR.Abstractions;

namespace WIS.Application.EventPublisher;

public class EventPublisher(IMedistR medistR) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default)
    {
        await medistR.PublishAsync(@event, ct);
    }
}