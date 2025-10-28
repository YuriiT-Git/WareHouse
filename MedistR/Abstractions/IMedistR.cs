namespace MedistR.Abstractions;

public interface IMedistR
{
    Task SendAsync(IRequest request, CancellationToken ct = default);

    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken ct = default);
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default);
}