using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Features;

public class StockUpdatedEventHandler(IInventoryAuditLogRepository repository) : IEventHandler<StockUpdatedEvent>
{
    public async Task Handle(StockUpdatedEvent evt, CancellationToken ct)
    {
        await repository.AddAsync(evt, ct);
    }
}