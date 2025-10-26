using MedistR.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetAllInventoryItems: IRequest<GetAllInventoryItems, IReadOnlyCollection<InventoryItemInfoDto>>
{
}