using WIS.Domain.ValueObjects.Enums;

namespace WIS.Application.DTO;

public class InventoryItemDto
{
    public required string Code { get; init; }
    public required ProductType ProductType { get; init; }
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required ItemSize Size { get; init; }
    public required string Color { get; init; }
    public required int Count { get; init; }
}