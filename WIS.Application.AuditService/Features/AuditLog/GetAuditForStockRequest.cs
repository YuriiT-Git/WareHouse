using MedistR.Abstractions;
using WIS.Application.AuditService.DTO;
using WIS.Domain.Events;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetAuditForStockRequest:IRequest<AuditLogDto>
{
    public required string Code { get; init; }
}