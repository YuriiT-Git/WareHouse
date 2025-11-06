using MedistR.Abstractions;
using WIS.Application.Common.Common.Abstractions;
using WIS.Domain.Abstractions;

namespace WIS.Application.Common.Features.GetStockLevelByDateTime;

public class GetStockLevelByDateTimeRequestHandler(IEventStorageRepository eventStorageRepository)
    : IRequestHandler<GetStockLevelByDateTimeRequest, IDomainEvent?>
{

    public async Task<IDomainEvent?> Handle(GetStockLevelByDateTimeRequest command, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(command.SkuNumber, nameof(command.SkuNumber));
        ArgumentNullException.ThrowIfNull(command.DateTime, nameof(command.DateTime));
        return await eventStorageRepository.GetByDateTimeAsync(command.SkuNumber, command.DateTime, cancellationToken);
    }
}