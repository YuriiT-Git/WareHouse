using MedistR.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Features.AuditLog;

public class GetAllInventoryItemsRequest: IRequest<IReadOnlyCollection<StockUpdatedEvent>>
{
}