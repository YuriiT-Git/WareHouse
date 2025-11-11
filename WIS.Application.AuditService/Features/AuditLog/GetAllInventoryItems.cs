using MedistR.Abstractions;
using WIS.Application.AuditService.DTO;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetAllInventoryItemsRequest: IRequest<IReadOnlyCollection<AuditLogDto>>
{
}