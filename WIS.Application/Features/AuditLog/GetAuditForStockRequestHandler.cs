using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Features.AuditLog;

public class GetAuditForStockRequestHandler(IInventoryAuditLogRepository repository)
    : IRequestHandler<GetAuditForStockRequest, StockUpdatedEvent>
{
    
    public async Task<StockUpdatedEvent> Handle(GetAuditForStockRequest command, CancellationToken cancellationToken)
    {
        return await repository.GetAsync(command.Code, cancellationToken);
    }
}