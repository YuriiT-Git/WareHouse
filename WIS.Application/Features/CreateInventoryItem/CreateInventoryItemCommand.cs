


using MedistR.Abstractions;

namespace WIS.Application.Features.CreateInventoryItem;

public class CreateInventoryItemCommand :  IRequest<CreateInventoryItemCommand, string>
{
    public required string ProductType { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string Size { get; set; }
    public required string Color { get; set; }
}