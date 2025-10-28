using MedistR.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WIS.Application.Features.CreateInventoryItem;

namespace WarehouseInventorySystem.Controllers;

[Route("api/inventory")]
public class InventoryController : ControllerBase
{
    private readonly IMedistR _medistR;

    public InventoryController(IMedistR medistR)
    {
        _medistR = medistR;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> CreateInventoryItem(
        [FromBody] CreateInventoryItemCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _medistR.SendAsync(command, cancellationToken);
        return Ok(result);
    }
}