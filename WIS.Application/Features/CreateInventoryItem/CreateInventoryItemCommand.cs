


using MedistR.Abstractions;
using WIS.Domain.ValueObjects.Enums;

namespace WIS.Application.Features.CreateInventoryItem;

public class CreateInventoryItemCommand :  IRequest<string>
{
    public required ProductType ProductType { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required ItemSize Size { get; set; }
    public required string Color { get; set; }
}