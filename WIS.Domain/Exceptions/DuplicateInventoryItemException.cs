public class DuplicateInventoryItemException : ApplicationException
{
    public DuplicateInventoryItemException(object code):base($"Duplicate inventory item code: {code}")
    {
    }
}