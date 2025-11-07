using WIS.Application.AuditService;
using WIS.Application.Common.Common.Abstractions;
using WIS.Domain.Events;
using WIS.Messaging.Abstractions;

namespace WIS.AuditService;

public class EventsConsumerService(
    IPulsarConsumer<StockUpdatedEvent> consumer,
    IServiceScopeFactory scopeFactory)
    : IHostedService, IAsyncDisposable
{
    private CancellationTokenSource _cts;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        consumer.Subscribe(OnMessage);
        return Task.CompletedTask;
    }

    private async Task OnMessage(StockUpdatedEvent obj)
    {
        var token = _cts?.Token ?? CancellationToken.None;
        await using var scope = scopeFactory.CreateAsyncScope();
        var repository = scope.ServiceProvider.GetRequiredService<IInventoryAuditLogRepository>();
        await repository.AddAsync(obj, token);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts?.Cancel();
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        _cts?.Dispose();
        await consumer.DisposeAsync();
    }
}