using WIS.Domain.ValueObjects.Enums;

namespace WIS.Domain.Events;

public class StockUpdatedEvent
{
    public required string Code { get; set; }
    public required ProductType ProductType { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required ItemSize Size { get; set; }
    public required string Color { get; set; }
    public required int Quantity { get; set; }
    public required DateTimeOffset UpdatedAt { get; set; }
}