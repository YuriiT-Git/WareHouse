using WIS.Domain.Events;
using WIS.Messaging.Abstractions;

namespace WIS.Application.Common.EventPublisher;

public class EventPublisher(IPulsarProducer<StockUpdatedEvent> producer) : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default)
    {
        await producer.ProduceAsync(@event as StockUpdatedEvent);
    }
}