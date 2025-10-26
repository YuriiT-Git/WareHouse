namespace MedistR.Abstractions;

public interface IMedistR
{
    Task SendAsync<TRequest>(IRequest<TRequest> request, CancellationToken ct = default);

    Task<TResponse> SendAsync<TRequest, TResponse>(IRequest<TRequest, TResponse> request,
        CancellationToken ct = default);
}