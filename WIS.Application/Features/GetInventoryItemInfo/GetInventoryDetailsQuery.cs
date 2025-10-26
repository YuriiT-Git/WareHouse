using MedistR.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetInventoryDetailsQuery: IRequest<GetInventoryDetailsQuery, InventoryItemInfoDto>
{
    public string Code { get; init; }
}