namespace WIS.Domain.Exceptions;

public class InventoryNotFoundException:ApplicationException
{
    public InventoryNotFoundException(string code):base($"Entity not found by SkuNumber {code}")
    {
    }
}