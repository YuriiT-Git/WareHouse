namespace WIS.Domain.Entities;

public class InventoryStock
{
    public int Quantity { get; private set; }
    
    private InventoryStock()
    {
    }

    public void RegisterIncoming(int count)
    {
        if (count == 0)
        {
            throw new InvalidOperationException("Inventory quantity must be greater than 0");
        }

        Quantity += count;
    }
    
    public void RegisterOutgoing(int count)
    {
        if (count > Quantity)
        {
            throw new InvalidOperationException("Inventory quantity is larger than the stock quantity");
        }
        
        if (count == 0)
        {
            throw new InvalidOperationException("Inventory quantity must be greater than 0");
        }

        Quantity -= count;
    }

    public static InventoryStock Create(int quantity)
    {
        return new InventoryStock { Quantity = quantity};
    }
}