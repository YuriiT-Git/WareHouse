using MedistR.Abstractions;
using WIS.Application.Common.Common.DTO;
using WIS.Domain.Abstractions;
using WIS.Domain.Events;

namespace WIS.Application.Common.Features.GetStockLevelByDateTime;

public class GetStockLevelByDateTimeRequest : IRequest<InventoryItemDto?>
{
    public required string SkuNumber { get; set; }
    public required DateTimeOffset DateTime { get; set; }
}