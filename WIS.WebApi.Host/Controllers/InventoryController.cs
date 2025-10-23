using Microsoft.AspNetCore.Mvc;
using Nexus;
using WIS.Application.Commands;
using WIS.Application.Queries;

namespace WarehouseInventorySystem.Controllers;

[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly INexus _nexus;

    public InventoryController(INexus nexus)
    {
        _nexus = nexus;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> CreateInventoryItemCommand(
        [FromBody] CreateInventoryItemCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _nexus.SendAsync(command, cancellationToken);
        return Ok(result);
    }
}