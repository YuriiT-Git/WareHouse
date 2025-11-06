using MedistR.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetAuditForStockRequest:IRequest<StockUpdatedEvent>
{
    public required string Code { get; init; }
}