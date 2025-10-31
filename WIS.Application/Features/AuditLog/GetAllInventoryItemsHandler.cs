using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Features.AuditLog;

public class GetAllInventoryItemsHandler(IInventoryAuditLogRepository auditLogRepository)
    : IRequestHandler<GetAllInventoryItemsRequest, IReadOnlyCollection<StockUpdatedEvent>>
{
    
    public async Task<IReadOnlyCollection<StockUpdatedEvent>> Handle(GetAllInventoryItemsRequest command, CancellationToken cancellationToken)
    {
        return await auditLogRepository.GetAllAsync(cancellationToken);
    }
}