
using Nexus.Abstractions;

namespace WIS.Application.Commands;

public class CreateInventoryItemCommand : ICommand<string>
{
    public required string ProductType { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string Size { get; set; }
    public required string Color { get; set; }
}