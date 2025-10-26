using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetAllInventoryItemsHandler(IInventoryItemRepository inventoryItemRepository)
    : IRequestHandler<GetAllInventoryItemsRequest, IReadOnlyCollection<InventoryItemInfoDto>>
{
    
    public async Task<IReadOnlyCollection<InventoryItemInfoDto>> Handle(GetAllInventoryItemsRequest command, CancellationToken cancellationToken)
    {
        return await inventoryItemRepository.GetAllInventoryItemAsync(cancellationToken);
    }
}