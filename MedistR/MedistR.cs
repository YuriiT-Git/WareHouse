using MedistR.Abstractions;

namespace MedistR;

public class MedistR(IServiceProvider serviceProvider) : IMedistR
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handler = serviceProvider.GetService(handlerType);

        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for {requestType}");
        }

        var method = handlerType.GetMethod("Handle");
        return await (Task<TResponse>)method.Invoke(handler, new object[] { request, ct });
    }
}