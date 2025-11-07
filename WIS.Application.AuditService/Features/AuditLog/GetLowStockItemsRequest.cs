using MedistR.Abstractions;
using WIS.Application.AuditService.DTO;
using WIS.Domain.Events;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetLowStockItemsRequest: IRequest<IReadOnlyCollection<AuditLogDto>>
{
    public int LessThan { get; set; } = 10;
}