using MedistR.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetAllInventoryItemsRequest: IRequest<GetAllInventoryItemsRequest, IReadOnlyCollection<InventoryItemInfoDto>>
{
}