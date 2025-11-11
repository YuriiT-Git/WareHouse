namespace WIS.Application.Common.EventPublisher;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default);
}