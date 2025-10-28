using MedistR.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MedistR;

public class MedistR(IServiceProvider serviceProvider) : IMedistR
{
    public async Task SendAsync<TRequest>(IRequest<TRequest> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(requestType);

        var result = GetResult(request, ct, handlerType, requestType);
        
        if (result is Task task)
        {
            await task;
        }
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var result = GetResult(request, ct, handlerType, requestType);
        
        return await (Task<TResponse>)result;
    }

    private object GetResult(object request, CancellationToken ct, Type handlerType,
        Type requestType)
    {
        var handler = serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for {requestType}");
        }

        var method = handlerType.GetMethod("Handle");

        if (method is null)
        {
            throw new InvalidOperationException($"Handler doesn't implement Handle method");
        }

        var result = method.Invoke(handler, [request, ct]);
        
        if (result is null)
        {
            throw new InvalidOperationException($"Handler '{method.Name}' returned null.");
        }
        
        return result;
    }
    
    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(@event);
        
        var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>();
  
        foreach (var handler in handlers)
        {
            await handler.Handle(@event, ct);
        }
    }
}