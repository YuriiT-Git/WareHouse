using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetInventoryItemsDetailsHandler(IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<GetInventoryDetailsRequest, InventoryItemDto>
{
    
    public async Task<InventoryItemDto> Handle(GetInventoryDetailsRequest command, CancellationToken cancellationToken)
    {
        return await inventoryItemRepository.GetInfoAsync(command.Code, cancellationToken);
    }
}