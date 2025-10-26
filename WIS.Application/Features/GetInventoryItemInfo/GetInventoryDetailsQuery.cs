using MedistR.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetInventoryDetailsRequest: IRequest<GetInventoryDetailsRequest, InventoryItemInfoDto>
{
    public string Code { get; init; }
}