using MedistR.Abstractions;

namespace MedistR;

public class MedistR(IServiceProvider serviceProvider) : IMedistR
{
    public async Task SendAsync<TRequest>(IRequest<TRequest> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(requestType);

        var handler = serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for {requestType}");
        }

        var method = handlerType.GetMethod("Handle");
        var result = method.Invoke(handler, new object[] { request, ct });

        if (result is Task task)
        {
            await task;
        }
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handler = serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for {requestType}");
        }

        var method = handlerType.GetMethod("Handle");
        return await (Task<TResponse>)method.Invoke(handler, [request, ct]);
    }
}