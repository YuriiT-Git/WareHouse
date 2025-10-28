namespace WarehouseInventorySystem.Models;

public class RegisterOutgoingStockModel
{
    public required string Code { get; set; }
    public int Quantity { get; set; }
}