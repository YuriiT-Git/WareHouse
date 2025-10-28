namespace WarehouseInventorySystem.Models;

public class RegisterIncomingStock
{
    public required string Code { get; set; }
    public required int Quantity { get; set; }
}