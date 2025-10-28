using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Features.AuditLog;

public class GetLowStockItemsRequestHandler(IInventoryAuditLogRepository repository)
    : IRequestHandler<GetLowStockItemsRequest, StockUpdatedEvent[]>
{
    
    public Task<StockUpdatedEvent[]> Handle(GetLowStockItemsRequest command, CancellationToken cancellationToken)
    {
        return repository.GetLowStockItemsAsync(command.LessThan, cancellationToken);
    }
}