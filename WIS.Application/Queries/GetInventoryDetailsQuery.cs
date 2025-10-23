using Nexus.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Queries;

public class GetInventoryDetailsQuery: ICommand<InventoryItemInfoDto>
{
    public string Code { get; init; }
}