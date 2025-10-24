namespace MedistR.Abstractions;

public interface IMedistR
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}