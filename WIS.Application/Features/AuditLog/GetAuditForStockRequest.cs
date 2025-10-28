using MedistR.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Features.AuditLog;

public class GetAuditForStockRequest:IRequest<GetAuditForStockRequest, StockUpdatedEvent[]>
{
    public required string Code { get; init; }
}