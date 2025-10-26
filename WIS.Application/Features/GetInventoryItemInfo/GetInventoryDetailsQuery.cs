using System.ComponentModel.DataAnnotations;
using MedistR.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Features.GetInventoryItemInfo;

public class GetInventoryDetailsRequest: IRequest<GetInventoryDetailsRequest, InventoryItemInfoDto>
{
    [Required]
    public required string Code { get; init; }
}