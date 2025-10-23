using Nexus.Abstractions;

namespace Nexus;

public class Nexus : INexus
{
    private readonly IServiceProvider _serviceProvider;

    public Nexus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handler = _serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for {requestType}");
        }

        var method = handlerType.GetMethod("Handle");
        return await (Task<TResponse>)method.Invoke(handler, new object[] { request, ct });
    }
}