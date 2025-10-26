using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetInventoryItemsDetailsHandler(IInventoryItemRepository inventoryItemRepository)
    : ICommandHandler<GetInventoryDetailsQuery, InventoryItemInfoDto>
{
    
    public async Task<InventoryItemInfoDto> Handle(GetInventoryDetailsQuery command, CancellationToken cancellationToken)
    {
        return await inventoryItemRepository.GetInventoryItemExtendedAsync(command.Code, cancellationToken);
    }
}