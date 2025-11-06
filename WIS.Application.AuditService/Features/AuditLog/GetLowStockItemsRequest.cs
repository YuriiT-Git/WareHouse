using MedistR.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.AuditService.Features.AuditLog;

public class GetLowStockItemsRequest: IRequest<IReadOnlyCollection<StockUpdatedEvent>>
{
    public int LessThan { get; set; } = 10;
}