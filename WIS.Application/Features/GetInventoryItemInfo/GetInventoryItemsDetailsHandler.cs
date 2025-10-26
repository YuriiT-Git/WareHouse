using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetInventoryItemsDetailsHandler(IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<GetInventoryDetailsRequest, InventoryItemInfoDto>
{
    
    public async Task<InventoryItemInfoDto> Handle(GetInventoryDetailsRequest command, CancellationToken cancellationToken)
    {
        return await inventoryItemRepository.GetInventoryItemExtendedAsync(command.Code, cancellationToken);
    }
}