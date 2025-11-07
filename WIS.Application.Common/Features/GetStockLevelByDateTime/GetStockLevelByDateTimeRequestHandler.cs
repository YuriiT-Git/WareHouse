using MedistR.Abstractions;
using WIS.Application.Common.Common.Abstractions;
using WIS.Application.Common.Common.DTO;

namespace WIS.Application.Common.Features.GetStockLevelByDateTime;

public class GetStockLevelByDateTimeRequestHandler(IEventStorageRepository eventStorageRepository)
    : IRequestHandler<GetStockLevelByDateTimeRequest, InventoryItemDto?>
{

    public async Task<InventoryItemDto?> Handle(GetStockLevelByDateTimeRequest command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(command.SkuNumber, nameof(command.SkuNumber));
        ArgumentNullException.ThrowIfNull(command.DateTime, nameof(command.DateTime));
        return await eventStorageRepository.GetByDateTimeAsync(command.SkuNumber, command.DateTime, cancellationToken);
    }
}