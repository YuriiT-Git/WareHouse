using MedistR.Abstractions;
using WIS.Application.Common.Abstractions;
using WIS.Application.DTO;
using WIS.Application.Queries;

namespace WIS.Application.Handlers;

public class GetAllInventoryItemsHandler(IInventoryItemRepository inventoryItemRepository)
    : ICommandHandler<GetAllInventoryItems, IReadOnlyCollection<InventoryItemInfoDto>>
{
    
    public async Task<IReadOnlyCollection<InventoryItemInfoDto>> Handle(GetAllInventoryItems command, CancellationToken cancellationToken)
    {
        return await inventoryItemRepository.GetAllInventoryItemAsync(cancellationToken);
    }
}