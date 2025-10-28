namespace MedistR.Abstractions;

public interface IEventHandler<in TEvent>
{
    public Task Handle(TEvent @event, CancellationToken ct);
}