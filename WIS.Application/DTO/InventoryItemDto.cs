using WIS.Domain.ValueObjects.Enums;

namespace WIS.Application.DTO;

public class InventoryItemDto
{
    public string Code { get; set; }
    public required ProductType ProductType { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required ItemSize Size { get; set; }
    public required string Color { get; set; }
}