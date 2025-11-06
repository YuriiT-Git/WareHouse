using MedistR.Abstractions;
using WIS.Application.Common.Common.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetLowStockItemsRequestHandler(IInventoryAuditLogRepository repository)
    : IRequestHandler<GetLowStockItemsRequest, IReadOnlyCollection<StockUpdatedEvent>>
{
    
    public Task<IReadOnlyCollection<StockUpdatedEvent>> Handle(GetLowStockItemsRequest command, CancellationToken cancellationToken)
    {
        return repository.GetLowStockItemsAsync(command.LessThan, cancellationToken);
    }
}