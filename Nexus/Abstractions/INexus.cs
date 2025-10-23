using Nexus.Abstractions;

namespace Nexus;

public interface INexus
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}