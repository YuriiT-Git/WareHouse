using Nexus.Abstractions;
using WIS.Application.DTO;

namespace WIS.Application.Queries;

public class GetAllInventoryItems: ICommand<IReadOnlyCollection<InventoryItemInfoDto>>
{
}