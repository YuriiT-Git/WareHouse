namespace WIS.Infrastructure.Entities;

public class InventoryStockModel
{
    public int Id { get; set; }
    public InventoryItemModel? InventoryItemModel { get; set; }
    public int Quantity { get; set; }
    public int? InventoryItemId { get; set; }
}