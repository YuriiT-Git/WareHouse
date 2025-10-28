using MedistR.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Features.AuditLog;

public class GetLowStockItemsRequest: IRequest<GetLowStockItemsRequest, StockUpdatedEvent[]>
{
    public int LessThan { get; set; } = 10;
}