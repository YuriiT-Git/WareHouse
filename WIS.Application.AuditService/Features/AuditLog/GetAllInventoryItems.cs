using MedistR.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetAllInventoryItemsRequest: IRequest<IReadOnlyCollection<StockUpdatedEvent>>
{
}